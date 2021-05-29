using FluentPOS.Application.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FluentPOS.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ExtendedIdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new ExtendedIdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new ExtendedIdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new ExtendedIdentityRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new ExtendedIdentityRole(Roles.Cashier.ToString()));
            await roleManager.CreateAsync(new ExtendedIdentityRole(Roles.Staff.ToString()));
        }
    }
}