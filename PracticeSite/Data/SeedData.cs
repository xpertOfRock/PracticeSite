using Microsoft.AspNetCore.Identity;
using PracticeSite.Data.Identity;

namespace PracticeSite.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            string adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Hideo Kojima",
                    Age = 30,
                    PhoneNumber = "+380123456789",
                    UserRole = UserRoles.Admin
                };

                var result = await userManager.CreateAsync(adminUser, "abobus123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }
            }

            string userEmail = "user@example.com";
            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    Name = "Sussy Baka",
                    Age = 25,
                    PhoneNumber = "+380987654321",
                    UserRole = UserRoles.User
                };

                var result = await userManager.CreateAsync(normalUser, "kebabus321");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, UserRoles.User);
                }
            }
        }
    }

}
