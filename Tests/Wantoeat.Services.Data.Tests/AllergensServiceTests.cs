namespace Wantoeat.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Data.Tests.Common;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;
    using Xunit;

    public class AllergensServiceTests
    {
        private const string FirstName = "First";
        private const string SecondName = "Second";

        public AllergensServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        // GetVMById
        [Fact]
        public async Task GetViewModelByIdShouldReturnCorrectId()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName });
            await dbContext.SaveChangesAsync();

            AllergenViewModel expectedVM = dbContext.Allergens.First().To<AllergenViewModel>();

            var service = new AllergensService(dbContext);
            AllergenViewModel actual = await service.GetViewModelByIdAsync<AllergenViewModel>(expectedVM.Id);

            Assert.True(expectedVM.Id == actual.Id);
        }

        [Fact]
        public async Task GetViewModelByIdShouldReturnCorrectViewModel()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName});
            dbContext.Allergens.Add(new Allergen { Name = SecondName });
            await dbContext.SaveChangesAsync();

            AllergenViewModel expectedVM = dbContext.Allergens.First().To<AllergenViewModel>();

            var service = new AllergensService(dbContext);
            AllergenViewModel actual = await service.GetViewModelByIdAsync<AllergenViewModel>(expectedVM.Id);


            Assert.IsType<AllergenViewModel>(actual);
        }

        [Fact]
        public async Task GetViewModelByIdShouldReturnNullIfIdDoesntExists()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            await dbContext.SaveChangesAsync();

            var service = new AllergensService(dbContext);
            AllergenViewModel actual = await service.GetViewModelByIdAsync<AllergenViewModel>(1);

            Assert.True(actual == null);
        }

        // GetAllToVM
        [Fact]
        public async Task GetAllToViewModelShouldReturnCorrectNumber()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName });
            dbContext.Allergens.Add(new Allergen { Name = SecondName });
            await dbContext.SaveChangesAsync();

            var expected = 2;

            var service = new AllergensService(dbContext);
            List<AllergenViewModel> actual = service.GetAllToViewModel<AllergenViewModel>().ToList();

            Assert.True(expected == actual.Count());
        }

        [Fact]
        public async Task GetAllToViewModelShouldReturnNullWhenEmptydB()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();

            var service = new AllergensService(dbContext);
            List<AllergenViewModel> actual = service.GetAllToViewModel<AllergenViewModel>().ToList();

            Assert.True(actual.Count() == 0);
        }

        [Fact]
        public async Task GetAllToViewModelShouldReturnCorrectListWithVM()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName, ImagePath = "www" });
            dbContext.Allergens.Add(new Allergen { Name = SecondName, ImagePath = "www" });
            await dbContext.SaveChangesAsync();

            List<AllergenSimpleViewModel> expected = new List<AllergenSimpleViewModel>
            {
                new AllergenSimpleViewModel { Id = 1, Name = "First", ImagePath = "www" },
                new AllergenSimpleViewModel { Id = 2, Name = "Second", ImagePath = "www" },
            };


            var service = new AllergensService(dbContext);
            List<AllergenSimpleViewModel> actual = service.GetAllToViewModel<AllergenSimpleViewModel>().ToList();

            for (int i = 0; i < expected.Count; i++)
            {
                var expectedEntry = expected[i];
                var actualEntry = actual[i];

                Assert.True(expectedEntry.Name == actualEntry.Name);
                Assert.True(expectedEntry.ImagePath == actualEntry.ImagePath);
            }
        }

        // GetAllToSelectListItem
        [Fact]
        public async Task GetAllToSelectListItemShouldReturnCorrectNumber()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName });
            dbContext.Allergens.Add(new Allergen { Name = SecondName });
            await dbContext.SaveChangesAsync();

            var expected = 2;

            var service = new AllergensService(dbContext);
            var actual = service.AllToSelectListItems().ToList();

            Assert.True(expected == actual.Count());
        }

        [Fact]
        public async Task GetAllToSelectListItemShouldReturnNullWhenEmptydB()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();

            var service = new AllergensService(dbContext);
            var actual = service.AllToSelectListItems().ToList();

            Assert.True(actual.Count() == 0);
        }

        [Fact]
        public async Task GetAllToSelectListItemShouldReturnCorrectListWithVM()
        {
            var dbContext = WantoeatDbContextInMemoryFactory.InitializeContext();
            dbContext.Allergens.Add(new Allergen { Name = FirstName, ImagePath = "www" });
            dbContext.Allergens.Add(new Allergen { Name = SecondName, ImagePath = "www" });
            await dbContext.SaveChangesAsync();

            List<SelectListItem> expected = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = FirstName },
                new SelectListItem { Value = "2", Text = SecondName },
            };


            var service = new AllergensService(dbContext);
            List<SelectListItem> actual = service.AllToSelectListItems().ToList();

            for (int i = 0; i < expected.Count; i++)
            {
                var expectedEntry = expected[i];
                var actualEntry = actual[i];

                Assert.True(expectedEntry.Text == actualEntry.Text);
            }
        }

    }
}
