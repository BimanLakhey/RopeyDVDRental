using GCW.Areas.Identity.Data;
using GCW.Enums;
using Microsoft.AspNetCore.Identity;
namespace GCW.Areas.Identity.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.assistant.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "sumit",
                Email = "sumit@gmail.com",
                FirstName = "sumit",
                LastName = "khatri",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "sum1t!1.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.assistant.ToString());
                }

            }
        }

    }
}