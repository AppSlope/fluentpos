using FluentPOS.Application.Enums;
using FluentPOS.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ExtendedIdentityUser> userManager)
        {
            var defaultUser = new ExtendedIdentityUser
            {
                UserName = DefaultIdentityConstants.DefaultStaffUser.UserName,
                Email = DefaultIdentityConstants.DefaultStaffUser.Email,
                FirstName = DefaultIdentityConstants.DefaultStaffUser.FirstName,
                LastName = DefaultIdentityConstants.DefaultStaffUser.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, DefaultIdentityConstants.DefaultStaffUser.Password);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Staff.ToString());
                }
            }
        }
    }
}