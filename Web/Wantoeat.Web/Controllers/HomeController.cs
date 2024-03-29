﻿namespace Wantoeat.Web.Controllers
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

        public HomeController(IRecipeService recipeService, IIngredientService ingredientService, IAllergensService allergensService)
        {
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
            this.allergensService = allergensService;
        }

        public async Task<IActionResult> Index()
        {
                var recipes = await this.recipeService.GetAllToViewModel<RecipeSimpleViewModel>().ToListAsync();

                return this.View(recipes);
        }

        public async Task<IActionResult> SearchByIngredients(int[] selected)
        {
            if (selected.Length > 0)
            {
                var matchedRecipes = await this.recipeService.GetRecipesByMatchingIngredients(selected).To<RecipeSimpleViewModel>().ToListAsync();

                var searchedIngredients = await this.ingredientService.GetAllToViewModelByIds<RecipeCreateIngredientViewModel>(selected).ToListAsync();

                var recipesViewModel = new RecipeAllViewModel { Recipes = matchedRecipes, Ingredients = searchedIngredients };

                return this.View("SearchResult", recipesViewModel);
            }

            var ingredients = this.ingredientService.GetAllToViewModel<IngredientSearchViewModel>().ToArray();

            SearchByIngredientsInputModel viewModel = new SearchByIngredientsInputModel { Ingredients = ingredients };

            return this.View(viewModel);
        }

        public async Task<IActionResult> SearchByAllergens(int[] selected)
        {
            if (selected.Length > 0)
            {
                var matchedRecipes = this.recipeService.GetAllNonContainingByAllergenId(selected);
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
