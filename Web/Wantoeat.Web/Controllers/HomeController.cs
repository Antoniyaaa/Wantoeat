namespace Wantoeat.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Data;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Ingredients;
    using Wantoeat.Web.ViewModels.Recipes;
    using Wantoeat.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;
        private readonly ApplicationDbContext dbContext;

        public HomeController(IRecipeService recipeService, IIngredientService ingredientService,
            ApplicationDbContext dbContext)
        {
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index([FromQuery]string criteria = null)
        {
                List<RecipeSimpleViewModel> recipes = await this.recipeService.GetAllToSimpleViewModel().ToListAsync(); ;

                this.ViewData["criteria"] = criteria;

                return this.View(recipes);
        }

        public async Task<IActionResult> SearchByIngredients(int[] selected)
        {
            if (selected.Length > 0)
            {
                var matchedRecipes = await this.recipeService.GetRecipesByIngredients(selected).To<RecipeSimpleViewModel>().ToListAsync();

                var searchedIngredients = await this.ingredientService.GetAllNamesByIds(selected).ToListAsync();

                var recipesViewModel = new RecipeAllViewModel { Recipes = matchedRecipes, Ingredients = searchedIngredients };

                return this.View("SearchResult", recipesViewModel);
            }

            var ingredients = this.ingredientService.GetAll().To<IngredientSearchViewModel>().ToArray();

            SearchByIngredientsInputModel viewModel = new SearchByIngredientsInputModel { Ingredients = ingredients };

            return this.View(viewModel);
        }

        public async Task<IActionResult> SortRecipes([FromQuery]string sortBy = "category")
        {
            var recipes = this.recipeService.GetGroups(sortBy);

            this.ViewData["sortBy"] = sortBy;

            return this.View(recipes);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
