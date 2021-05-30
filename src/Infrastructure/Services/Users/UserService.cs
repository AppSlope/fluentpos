﻿using AutoMapper;
using FluentPOS.Application.Enums;
using FluentPOS.Application.Interfaces.Services.Users;
using FluentPOS.Application.Requests.Shared;
using FluentPOS.Application.Requests.Users;
using FluentPOS.Application.Responses.Users;
using FluentPOS.Infrastructure.Identity;
using FluentPOS.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly RoleManager<ExtendedIdentityRole> _roleManager;

        public UserService(
            UserManager<ExtendedIdentityUser> userManager,
            IMapper mapper,
            RoleManager<ExtendedIdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        private IMapper _mapper;

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<List<UserResponse>>(users);
            return Result<List<UserResponse>>.Success(result);
        }

        public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                return Result.Fail($"Username '{request.UserName}' is already taken.");
            }
            var user = new ExtendedIdentityUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.ActivateUser,
                EmailConfirmed = request.AutoConfirmEmail
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Staff.ToString());
                    if (!request.AutoConfirmEmail)
                    {
                        var verificationUri = await SendVerificationEmail(user, origin);
                        //BackgroundJob.Enqueue(() => _mailService.SendAsync(new MailRequest() { From = "mail@codewithmukesh.com", To = user.Email, Body = $"Please confirm your account by <a href='{verificationUri}'>clicking here</a>.", Subject = "Confirm Registration" }));
                        return Result<int>.Success(user.Id, message: $"User Registered. Please check your Mailbox to verify!");
                    }
                    return Result<int>.Success(user.Id, message: $"User Registered");
                }
                else
                {
                    return Result.Fail(result.Errors.Select(a => a.Description).ToList());
                }
            }
            else
            {
                return Result.Fail($"Email {request.Email } is already registered.");
            }
        }

        private async Task<string> SendVerificationEmail(ExtendedIdentityUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/identity/user/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }

        public async Task<IResult<UserResponse>> GetAsync(int userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var result = _mapper.Map<UserResponse>(user);
            return Result<UserResponse>.Success(result);
        }

        public async Task<IResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRoleModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var result = new UserRolesResponse { UserRoles = viewModel };
            return Result<UserRolesResponse>.Success(result);
        }

        public async Task<IResult<int>> ConfirmEmailAsync(int userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Result<int>.Success(user.Id, message: $"Account Confirmed for {user.Email}.You can now use the /api/identity/token endpoint to generate JWT.");
            }
            else
            {
                throw new Exception($"An error occured while confirming user.Email.");
            }
        }

        public async Task<IResult> ForgotPasswordAsync(string emailId, string origin)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Result.Fail("An Error has occured!");
            }
            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "account/reset-password";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var passwordResetURL = QueryHelpers.AddQueryString(_enpointUri.ToString(), "Token", code);
            var request = new MailRequest()
            {
                Body = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(passwordResetURL)}'>clicking here.</a>.",
                Subject = "Reset Password",
                To = emailId
            };
            //BackgroundJob.Enqueue(() => _mailService.SendAsync(request));
            return Result.Success("Password Reset Mail has been sent to your authorized EmailId.");
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return Result.Fail("An Error has occured!");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return Result.Success("Password Reset Successful!");
            }
            else
            {
                return Result.Fail("An Error has occured!");
            }
        }
    }
}