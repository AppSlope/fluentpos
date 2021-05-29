using FluentPOS.Application.Enums;
using FluentPOS.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdministrator
    {
        public static async Task SeedAsync(UserManager<ExtendedIdentityUser> userManager)
        {
            var defaultUser = new ExtendedIdentityUser
            {
                UserName = DefaultIdentityConstants.DefaultSuperAdministrator.UserName,
                Email = DefaultIdentityConstants.DefaultSuperAdministrator.Email,
                FirstName = DefaultIdentityConstants.DefaultSuperAdministrator.FirstName,
                LastName = DefaultIdentityConstants.DefaultSuperAdministrator.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, DefaultIdentityConstants.DefaultSuperAdministrator.Password);
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Cashier.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Staff.ToString());
                }
            }
        }
    }
}