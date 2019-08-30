using System;
using System.Collections.Generic;
using System.Text;
using Wantoeat.Data.Models;
using System.Linq;
using Wantoeat.Data;
using System.Threading.Tasks;
using Wantoeat.Services.Data.Tests.Common;
using Xunit;

namespace Wantoeat.Services.Data.Tests
{
    public class RecipesServiceTests
    {
        public RecipesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        private List<Recipe> GetRecipes()
        {
            var allergenGluten = new Allergen { Name = "Gluten" };
            var allergenMilk = new Allergen { Name = "Milk" };

            var ingredientFlour = new Ingredient
            {
                Name = "Flour",
                IngredientAllergens = new List<IngredientAllergen>
                {
                    new IngredientAllergen { Allergen = allergenGluten},
                }
            };

            var ingredientTomatoes = new Ingredient { Name = "Tomatoes" };
            var ingredientPepperoni = new Ingredient { Name = "Pepperoni" };

            var ingredientParmesan = new Ingredient
            {
                Name = "Parmesan",
                IngredientAllergens = new List<IngredientAllergen>
                {
                    new IngredientAllergen { Allergen = allergenMilk }
                }
            };
            
            var recipes = new List<Recipe>()
            {
            new Recipe
            {
                Name = "Creamy Avocado Egg Salad",
                Description = "Pit and peel.",
                ImagePath = "/images/Creamy Avocado Egg Salad_9931526.jpg",
                Category = new Category { Name = "Vegetarian"},
                CookingTime = new CookingTime { Name = "15 min."},
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = new ,
                            Quantity = "3 pcs." },
                        new RecipeIngredient { Ingredient = mayo,
                            Quantity = "2 tablespoons" },
                        new RecipeIngredient { Ingredient = lemon,
                            Quantity = "1 teaspoon juice" },
                         new RecipeIngredient { Ingredient = avocado,
                            Quantity = "1 pcs." },
                    },
                RecipeAllergens = new List<RecipeAllergen>
                    {
                        new RecipeAllergen { Allergen = egg },
                    }
            },

            new Recipe
            {
                Name = "Crispy Parmesan Crusted Chicken",
                Description = "Grate the parmesan cheese. Lay the chicken breasts out on a cutting board and cut in half horizontally. Generously season with Italian seasoning (or seasoning of choice), garlic powder, salt, and pepper; Set aside." +
                "Combine the parmesan cheese and almond flour in a medium shallow bowl.In another bowl," +
                "whisk the eggs.Dip the chicken breasts into the egg mixture then into the parmesan mixture.shake off the excess breading.Repeat until all the chicken cutlets are covered.Heat oil or butter in a large non - stick heavy duty pan.Add chicken cutlets in a single layer and cook for 5 - 6 minutes on each side, until golden and crispy.Be sure not to flip until the parmesan is golden on the first side or it will slide off.Repeat with remaining chicken cutlets." +
                "Source: www.gimmedelicious.com.",
                ImagePath = "/images/Crispy Parmesan Crusted Chicken_61277109.jpg",
                Category = meatCategory,
                CookingTime = cookingTime15,
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = oliveOil,
                            Quantity = "3 tablespoons" },
                        new RecipeIngredient { Ingredient = salt, Quantity = "On taste"},
                        new RecipeIngredient { Ingredient = pepper, Quantity = "On taste"}
                    },
            };
        };

            return recipes;
        }

        private async Task SeedData(ApplicationDbContext context)
        {
            context.AddRange(GetRecipes());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetAllNonContainingByAllergenId_ShouldReturnCorrectResults()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new RecipeService(dbContext);
        }
    }
}
