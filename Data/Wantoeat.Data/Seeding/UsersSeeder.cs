namespace Wantoeat.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Wantoeat.Common;
    using Wantoeat.Data.Models;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManager, GlobalConstants.AdministratorEmail, GlobalConstants.AdministratorPassword);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string adminEmail, string adminPassword)
        {
            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(user, adminPassword);
                var resultRole = await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

                // TODO Check what is the error message here
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
