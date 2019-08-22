namespace Wantoeat.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            IEnumerable<Category> entities = new List<Category>
            {
                new Category { Name = "Soups"},
                new Category { Name = "Salads"},
                new Category { Name = "Pasta"},
                new Category { Name = "Meat"},
                new Category { Name = "Vegetarian"},
                new Category { Name = "Vegan"},
                new Category { Name = "Desserts"},
                new Category { Name = "Drinks"},
            };

            await dbContext.Categories.AddRangeAsync(entities);
        }
    }
}
