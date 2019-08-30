namespace Wantoeat.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Services.Data.Tests.Common;
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipesServiceTests
    {
        public RecipesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        private List<Recipe> GetRecipes()
        {
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
                        new RecipeIngredient { Ingredient = new Ingredient { Name = "Eggs"},
                            Quantity = "3 pcs." },
                        new RecipeIngredient { Ingredient = new Ingredient { Name = "Mayo"},
                            Quantity = "2 tablespoons" },
                         new RecipeIngredient { Ingredient = new Ingredient { Name = "Avocado"},
                            Quantity = "1 pcs." },
                    },
                RecipeAllergens = new List<RecipeAllergen>
                    {
                        new RecipeAllergen { Allergen = new Allergen { Name = "Eggs" } },
                    }
            },

            new Recipe
            {
                Name = "Crispy Parmesan Crusted Chicken",
                Description = "Grate",
                ImagePath = "/images/Crispy Parmesan Crusted Chicken_61277109.jpg",
                Category = new Category { Name = "Meat"},
                CookingTime = new CookingTime { Name = "15 min."},
                RecipeIngredient = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { Ingredient = new Ingredient { Name = "Olive oil"},
                            Quantity = "3 tablespoons" },
                        new RecipeIngredient { Ingredient = new Ingredient { Name = "Salt"},
                            Quantity = "On taste"},
                        new RecipeIngredient { Ingredient = new Ingredient { Name = "Pepper"},
                            Quantity = "On taste"},
                    },
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
        public async Task GetAllNonContainingByAllergenId_ShouldReturnCorrectResults()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var allergen = dbContext.Allergens.First();
            var expected = dbContext.Recipes.First();
            var ids = new int[] { allergen.Id };

            var service = new RecipeService(dbContext);
            var actual = service.GetAllNonContainingByAllergenId(ids).ToList();

            Assert.True(expected != actual.First());
        }

        [Fact]
        public async Task GetAllNonContainingByAllergenId_WithExistingAndNonExistingAllergenIds_ShouldReturnCorrectResults()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var allergen = dbContext.Allergens.First();
            var recipe = dbContext.Recipes.First();
            var ids = new int[] { 20, allergen.Id, 10 };

            var service = new RecipeService(dbContext);
            var actual = service.GetAllNonContainingByAllergenId(ids).ToList();

            Assert.True(recipe != actual.First());
        }

        [Fact]
        public async Task GetAllNonContainingByAllergenId_WithZeroAllergenIds_ShouldReturnAllRecipes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new RecipeService(dbContext);
            var exptected = dbContext.Recipes.ToList();

            var ids = new int[2];
            var actual = service.GetAllNonContainingByAllergenId(ids).ToList();
            
            Assert.True(actual.Count == exptected.Count);
        }

        [Fact]
        public async Task GetGroupsByCategories_WithNoGroupsInDb_ShouldReturnListWithAllRecipes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();

            var recipe = new Recipe
            {
                Name = "Creamy Avocado Egg Salad",
                Description = "Pit and peel.",
                ImagePath = "/images/Creamy Avocado Egg Salad_9931526.jpg",
                CookingTime = new CookingTime { Name = "15 min." },
            };

            dbContext.Recipes.Add(recipe);
            await dbContext.SaveChangesAsync();

            var service = new RecipeService(dbContext);

            var result = service.GetGroupsByCategories();

            Assert.True(result.Count == dbContext.Recipes.Count());
        }

        [Fact]
        public async Task GetGroupsByCategories_ShouldReturnCorrectGroupNames()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new RecipeService(dbContext);

            var expected = dbContext.Categories.Select(x => x.Name).ToList();

            var result = service.GetGroupsByCategories();

            for (int i = 0; i < result.Count(); i++)
            {
                Assert.True(result[i].GroupName == expected[i]);
            }
        }

        [Fact]
        public async Task GetGroupsByCategories_ShouldReturnCorrectViewModelInList()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new RecipeService(dbContext);

            var result = service.GetGroupsByCategories();

            RecipeSimpleWithCategoryViewModel expectedVM = dbContext.Recipes.First().To<RecipeSimpleWithCategoryViewModel>();

            Assert.IsType<RecipeSimpleWithCategoryViewModel>(result.First().Recipes.First());
        }

        [Fact]
        public async Task GetByMatchingIngredinets_ShouldReturnTheRightRecipes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var ingredient = dbContext.Ingredients.First();
            var ids = new int[] { ingredient.Id };
            var recipe = dbContext.Recipes.First();

            var service = new RecipeService(dbContext);
            var actual = service.GetRecipesByMatchingIngredients(ids);

            Assert.True(actual.Count() == 1);
            Assert.True(recipe.Name == actual.First().Name);
        }

        [Fact]
        public async Task GetByMatchingIngredinets_ShouldReturnRecipesInRightOrder()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var firstRecipe = dbContext.Recipes.First();

            var ingredientIds = firstRecipe.RecipeIngredient.Select(x => x.IngredientId).ToArray();
            var testRecipe = new Recipe { Name = "Test" };
            for (int i = 1; i < ingredientIds.Count(); i++)
            {
                testRecipe.RecipeIngredient.Add(new RecipeIngredient { IngredientId = ingredientIds[i] });
            }
            dbContext.Recipes.Add(testRecipe);
            await dbContext.SaveChangesAsync();

            var expected = new List<Recipe> { firstRecipe, testRecipe };

            var service = new RecipeService(dbContext);
            var actual = service.GetRecipesByMatchingIngredients(ingredientIds).ToList();

            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public async Task GetByCategory_ShouldReturnTheRightRecipes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var category = dbContext.Categories.First().Name;
            var expected = dbContext.Recipes.Where(x => x.Category.Name == category).First();

            var service = new RecipeService(dbContext);
            var actual = service.GetByCategory(category).ToList();

            Assert.True(actual.Count() == 1);
            Assert.True(expected == actual.First());
        }

        [Fact]
        public async Task GetByCategory_WithNullCategory_ShouldReturnAllRecipes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var expected = dbContext.Recipes;

            var service = new RecipeService(dbContext);
            var actual = service.GetByCategory(null);

            Assert.Equal(expected, actual);
        }

    }
}
