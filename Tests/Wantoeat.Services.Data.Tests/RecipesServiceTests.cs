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
                    Name = "Spaghetti",
                    Category = new Category
                    {
                         Name = "Pasta",
                    },
                    CookingTime = new CookingTime
                    {
                        Name = "30 Min",
                    },
                    RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient {Ingredient = ingredientFlour},
                        new RecipeIngredient {Ingredient = ingredientParmesan},
                        new RecipeIngredient {Ingredient = ingredientTomatoes}
                    }
                },
                new Recipe
                {
                    Name = "Pizza",
                    Category = new Category
                    {
                         Name = "Pasta",
                    },
                    CookingTime = new CookingTime
                    {
                        Name = "30 Min",
                    },
                    RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient {Ingredient = ingredientFlour},
                        new RecipeIngredient {Ingredient = ingredientParmesan},
                        new RecipeIngredient {Ingredient = ingredientTomatoes},
                        new RecipeIngredient {Ingredient = ingredientPepperoni}
                    }
                }
            };

            return recipes;
        }

        private async Task SeedData(ApplicationDbContext context)
        {
            context.AddRange(GetRecipes());
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task CreateShouldCreateSuccessfully()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new RecipeService(dbContext);

        }
    }
}
