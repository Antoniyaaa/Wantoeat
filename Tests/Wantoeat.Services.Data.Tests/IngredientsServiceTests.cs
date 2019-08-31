namespace Wantoeat.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Services.Data.Tests.Common;
    using Wantoeat.Web.ViewModels.Ingredients;

    using Xunit;

    public class IngredientsServiceTests
    {
        private List<Ingredient> GetIngredients()
        {
            var allergenGluten = new Allergen { Name = "Gluten" };
            var allergenMilk = new Allergen { Name = "Milk" };

            var ingredients = new List<Ingredient>
            {
            new Ingredient
            {
                Name = "Flour",
                IngredientAllergens = new List<IngredientAllergen>
                {
                    new IngredientAllergen { Allergen = allergenGluten},
                }
            },
            new Ingredient
            {
                Name = "Tomatoes",
                Description = "Some description",
                ImagePath = "www....",
            },
            new Ingredient { Name = "Pepperoni" },
            new Ingredient
            {
                Name = "Parmesan",
                IngredientAllergens = new List<IngredientAllergen>
                {
                    new IngredientAllergen { Allergen = allergenMilk }
                },
                Description = "Some description",
                ImagePath = "www...."
            }};

            return ingredients;
        }

        private async Task SeedData(ApplicationDbContext context)
        {
            context.AddRange(GetIngredients());
            await context.SaveChangesAsync();
        }

        public IngredientsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task Create_WithoutAllergens_ShouldBeSucessfull()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var ingredientVM = new IngredientCreateInputModel
            {
                Name = "Flour"
            };

            var craeted = await service.CreateAsync(ingredientVM);

            Assert.NotNull(craeted);
        }

        [Fact]
        public async Task Create_FromViewModel_ShouldReturnRightType()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var ingredientVM = new IngredientCreateInputModel
            {
                Name = "Flour"
            };

            Ingredient actual = await service.CreateAsync(ingredientVM);

            Assert.IsType<Ingredient>(actual);
        }

        [Fact]
        public async Task Create_WithExistingAllergens_ShouldCreateAlsoRelationToAllergens()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var allergenGluten = new Allergen { Name = "Gluten" };
            var allergenLupin = new Allergen { Name = "Lupin" };
            dbContext.Add(allergenGluten);
            dbContext.Add(allergenLupin);
            await dbContext.SaveChangesAsync();

            var ingredientVM = new IngredientCreateInputModel
            {
                Name = "Flour",
                AllergenIds = new List<int>
                {
                    allergenGluten.Id, allergenLupin.Id
                }
            };

            var actual = await service.CreateAsync(ingredientVM);

            Assert.True(actual.IngredientAllergens.Count == 2);
        }

        [Fact]
        public async Task Create_WithExistingAndNonExistingAllergens_ShouldReturnNull()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var allergenGluten = new Allergen { Name = "Gluten" };
            dbContext.Add(allergenGluten);
            await dbContext.SaveChangesAsync();

            var ingredientVM = new IngredientCreateInputModel
            {
                Name = "Flour",
                AllergenIds = new List<int>
                {
                    allergenGluten.Id, 2
                }
            };

            var actual = await service.CreateAsync(ingredientVM);

            Assert.True(actual == null);
        }

        [Fact]
        public async Task DeleteById_WithNotExistingId_ShouldReturnFalse()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var result = await service.DeleteByIdAsync(10);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteById_WithExistingId_ShouldReturnTrue()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);
            var service = new IngredientsService(dbContext);

            var test = dbContext.Ingredients.First();
            var result = await service.DeleteByIdAsync(test.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteById_ShouldSetDeletedPropertyTrue()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);
            var service = new IngredientsService(dbContext);

            var countBeforeDeletion = dbContext.Ingredients.Count();

            var test = dbContext.Ingredients.First();
            await service.DeleteByIdAsync(test.Id);
            await dbContext.SaveChangesAsync();

            var countAfterDeletion = dbContext.Ingredients.Count();

            Assert.True(countAfterDeletion == (countBeforeDeletion - 1));
        }

        [Fact]
        public async Task GetUnused_ShouldReturnCorrectCount()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);
            var ingredient = dbContext.Ingredients.First();
            var recipe = new Recipe
            {
                Name = "Test",
                RecipeIngredient = new List<RecipeIngredient>
                {
                    new RecipeIngredient { Ingredient = ingredient }
                }
            };
            dbContext.Recipes.Add(recipe);
            await dbContext.SaveChangesAsync();

            var service = new IngredientsService(dbContext);

            var expected = GetIngredients().Count();
            var actual = service.GetAllUnused(recipe.Id).Count();

            Assert.True(actual == (expected - 1));
        }

        [Fact]
        public async Task GetViewModelById_ShouldReturnCorrectId()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var expectedVM = dbContext.Ingredients.First().To<IngredientEditInputModel>();

            var service = new IngredientsService(dbContext);
            var actual = await service.GetViewModelByIdAsync<IngredientEditInputModel>(expectedVM.Id);

            Assert.True(expectedVM.Id == actual.Id);
        }

        [Fact]
        public async Task GetViewModelByIdShouldReturnCorrectViewModel()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            IngredientEditInputModel expectedVM = dbContext.Ingredients.First().To<IngredientEditInputModel>();

            var service = new IngredientsService(dbContext);
            IngredientEditInputModel actual = await service.GetViewModelByIdAsync<IngredientEditInputModel>(expectedVM.Id);


            Assert.IsType<IngredientEditInputModel>(actual);
        }

        [Fact]
        public async Task GetViewModelById_ShouldReturnNull_IfIdDoesntExists()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();

            var service = new IngredientsService(dbContext);
            IngredientEditInputModel actual = await service.GetViewModelByIdAsync<IngredientEditInputModel>(1);

            Assert.True(actual == null);
        }

        [Fact]
        public async Task Edit_WithNonExistingId_ShouldReturnNull()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            var service = new IngredientsService(dbContext);

            var tested = new IngredientEditInputModel { Id = 1 };

            var result = await service.EditAsync(tested);

            Assert.Null(result);
        }

        [Fact]
        public async Task Edit_WithExistingId_ShouldReturnTheRightResult()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new IngredientsService(dbContext);

            var actual = dbContext.Ingredients.First();

            var tested = new IngredientEditInputModel { Id = actual.Id };

            var result = await service.EditAsync(tested);

            Assert.True(actual.Id == result.Id);
        }

        [Fact]
        public async Task Edit_WithNoNewAllergens_ShouldNotChangeTheOldOnes()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new IngredientsService(dbContext);

            var actual = dbContext.Ingredients.First();

            actual.Name = "Wheat flour";
            actual.ImagePath = "www";
            await dbContext.SaveChangesAsync();

            var tested = new IngredientEditInputModel { Id = actual.Id };

            var result = await service.EditAsync(tested);

            Assert.True(actual.IngredientAllergens.Count() == result.IngredientAllergens.Count());
        }

        [Fact]
        public async Task Edit_WithAddingNewAllergens_ShouldBeSuccessful()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var service = new IngredientsService(dbContext);

            var entityBeforeEdit = dbContext.Ingredients.First();
            var countBeforeEdit = entityBeforeEdit.IngredientAllergens.Count();
            var newAllergen = dbContext.Allergens.Last();

            var test = new IngredientEditInputModel
            {
                Id = entityBeforeEdit.Id,
                AllergenNames = new List<string> { newAllergen.Name} };

            var entityAfterEdit = await service.EditAsync(test);
            var countAfterEdit = entityAfterEdit.IngredientAllergens.Count();

            Assert.True(countAfterEdit == (countBeforeEdit + 1));
        }

        [Fact]
        public async Task GetAllToViewModelShouldReturnCorrectNumber()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);
            var expected = dbContext.Ingredients.Count();

            var service = new IngredientsService(dbContext);
            var actual = service.GetAllToViewModel<IngredientSimpleViewModel>().ToList();

            Assert.True(expected == actual.Count());
        }

        [Fact]
        public async Task GetAllToViewModelShouldReturnCorrectListWithVM()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await SeedData(dbContext);

            var expected = new List<IngredientSimpleViewModel>
            {
                new IngredientSimpleViewModel { Name = "Flour"},
                new IngredientSimpleViewModel { Name = "Tomatoes"},
                new IngredientSimpleViewModel { Name = "Pepperoni"},
                new IngredientSimpleViewModel { Name = "Parmesan"},
            };

            var service = new IngredientsService(dbContext);
            var actual = service.GetAllToViewModel<IngredientSimpleViewModel>().ToList();

            for (int i = 0; i < expected.Count; i++)
            {
                var expectedEntry = expected[i];
                var actualEntry = actual[i];

                Assert.True(expectedEntry.Name == actualEntry.Name);
            }
        }
    }
}