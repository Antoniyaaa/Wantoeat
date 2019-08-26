using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wantoeat.Data.Models;

namespace Wantoeat.Data.Seeding
{
    internal class IngredientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ingredients.Any())
            {
                return;
            }

            IEnumerable<Ingredient> entities = new List<Ingredient>
            {
                new Ingredient{ Name = "Potatoes", Description = "Carbs are the main dietary component of potatoes. " +
                "Those cooled down after boiling may provide some resistant starch, which can improve gut health. " +
                "Potatoes are a good source of several vitamins and minerals, including potassium, folate, and " +
                "vitamins C and B6. Source: www.healthline.com", ImagePath = "/images/Potatoes_42474043.jpg", },
                new Ingredient{ Name = "Mozzarella", Description = "Mozzarella is a soft and mild-tasting fresh cheese. " +
                "It offers a decent nutritional profile with particularly high amounts of protein, calcium, " +
                "phosphorus, and vitamin B12. Mozzarella is very low in calories compared to most other cheese varieties." +
                "It provides more than half the daily recommended value for calcium per 100 grams. " +
                "Source: www.nutritionadvance.com", ImagePath = "/images/Mozzarella_30610886.jpg",
                IngredientAllergens = new List<IngredientAllergen> { new IngredientAllergen { AllergenId = 7 } } }
            };

            await dbContext.Ingredients.AddRangeAsync(entities);
        }
    }
}
