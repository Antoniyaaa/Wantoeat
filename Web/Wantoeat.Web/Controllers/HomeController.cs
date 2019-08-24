namespace Wantoeat.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Services.Data;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;
    using Wantoeat.Web.ViewModels.Ingredients;
    using Wantoeat.Web.ViewModels.Recipes;
    using Wantoeat.Web.ViewModels.Search;

    public class HomeController : BaseController
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientService ingredientService;
        private readonly IAllergensService allergensService;
        private readonly ICategoryService categoryService;

        public HomeController(IRecipeService recipeService, IIngredientService ingredientService, IAllergensService allergensService,
            ICategoryService categoryService)
        {
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
            this.allergensService = allergensService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index([FromQuery]string criteria = null)
        {
                var recipes = await this.recipeService.GetAllToViewModel<RecipeSimpleViewModel>().ToListAsync();

                this.ViewData["criteria"] = criteria;

                return this.View(recipes);
        }

        public async Task<IActionResult> SearchByIngredients(int[] selected)
        {
            if (selected.Length > 0)
            {
                var matchedRecipes = await this.recipeService.GetRecipesByIngredients(selected).To<RecipeSimpleViewModel>().ToListAsync();

                var searchedIngredients = await this.ingredientService.GetAllToViewModelByIds<RecipeCreateIngredientViewModel>(selected).ToListAsync();

                var recipesViewModel = new RecipeAllViewModel { Recipes = matchedRecipes, Ingredients = searchedIngredients };

                return this.View("SearchResult", recipesViewModel);
            }

            var ingredients = this.ingredientService.GetAllToViewModel<IngredientSearchViewModel>().ToArray();

            SearchByIngredientsInputModel viewModel = new SearchByIngredientsInputModel { Ingredients = ingredients };

            return this.View(viewModel);
        }

        public async Task<IActionResult> SortRecipes([FromQuery]string sortBy = "category")
        {
            var recipes = this.recipeService.GetGroups(sortBy);

            this.ViewData["sortBy"] = sortBy;

            return this.View(recipes);
        }

        public async Task<IActionResult> AllRecipes([FromQuery]string category = null) //, int[] selected)
        {
            var recipes = this.recipeService.GetByCategory(category).To<RecipeSimpleViewModel>().ToList();

            this.ViewData["category"] = category;

            this.ViewData["allergens"] = this.allergensService.GetAllToViewModel<AllergenSimpleViewModel>().Select(x => x.Name);

            /*if (selected.Length > 0)
            {
                recipes = this.recipeService.GetAllNonContainingAllergen(selected).To<RecipeSimpleViewModel>().ToList();
            }*/

            /*if (criteria != null)
            {
                // Ако има избрана някоя (1) категория
                // то само алергените от нея трябва да бъдат показани
            }*/

            //this.ViewData["allergens"] = this.allergensService.GetAllToViewModel<AllergenSimpleViewModel>();


            return this.View(recipes);
        }

        public async Task<IActionResult> SearchByAllergens(int[] selected)
        {
            if (selected.Length > 0)
            {
                var matchedRecipes = this.recipeService.GetAllNonContainingAllergen(selected);
                var blq = matchedRecipes.To<RecipeSimpleViewModel>().AsEnumerable();

                var searchedAllergens = await this.allergensService.GetAllToViewModelByIds<AllergenSimpleViewModel>(selected).ToListAsync();

                var recipesViewModel = new RecipeAllViewModel { Recipes = blq, Allergens = searchedAllergens };

                return this.View("SearchResult", recipesViewModel);
            }

            var allergens = this.allergensService.GetAllToViewModel<AllergenSimpleViewModel>().ToList();

            return this.View(allergens);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => this.View();
    }
}
