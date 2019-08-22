using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wantoeat.Services.Data;
using Wantoeat.Web.ViewModels.Ingredients;
using Wantoeat.Web.ViewModels.Search;
using Wantoeat.Services.Mapping;
using Wantoeat.Web.ViewModels.Recipes;
using System;
using Wantoeat.Data;
using System.Collections.Generic;
using Wantoeat.Data.Models;

namespace Wantoeat.Web.Controllers
{
    public class SearchController : Controller
    {
        /*private readonly IIngredientService ingredientService;
        private readonly IRecipeService recipeService;
        private readonly ApplicationDbContext dbContext;
        private readonly ISearchService searchService;

        public SearchController(IIngredientService ingredientService, IRecipeService recipeService
            , ApplicationDbContext dbContext, ISearchService searchService)
        {
            this.ingredientService = ingredientService;
            this.recipeService = recipeService;
            this.dbContext = dbContext;
            this.searchService = searchService;
        }

        public IActionResult Index(string searchString, string recipeCategory)
        {
            IQueryable<string> categoryQuery = this.dbContext.Recipes.OrderBy(r => r.CategoryId).Select(r => r.Category.Name);

            var recipes = this.recipeService.All();

            if (!string.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(recipeCategory))
            {
                recipes = recipes.Where(r => r.Category.Name == recipeCategory);
            }

            var recipesQue = recipes.AsQueryable().To<RecipeViewModel>().ToList();

            SearchByCategoryViewModel viewModel = new SearchByCategoryViewModel
            {
                 Recipes = recipesQue,                                                                                                  // await recipes.ToListAsync()
                 Categories = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await categoryQuery.Distinct().ToList())        // await categoryQuery...ToListAsync
            };

            return this.View(viewModel);
        }

        public IActionResult SearchByIngredients(int[] selected)
        {
            var ingredients = this.ingredientService.All().To<IngredientSearchViewModel>().ToArray();

            if (selected.Length > 0)
            {
                var newList = new List<Ingredient>();

                for (int i = 0; i < selected.Length; i++)
                {
                    var ingredient = this.ingredientService.GetById(selected[i]);
                    newList.Add(ingredient);
                }
             ingredients = newList.AsQueryable().To<IngredientSearchViewModel>().ToArray();

                var matchedRecipes = this.searchService.GetRecipesByIngredients(selected).To<RecipeViewModel>().ToList();

                RecipeAllViewModel RecipesviewModel = new RecipeAllViewModel { Recipes = matchedRecipes };

                return this.View("Result", RecipesviewModel);
                return this.View();
            }

            SearchByIngredientsInputModel viewModel = new SearchByIngredientsInputModel { Ingredients = ingredients };

            return this.View(viewModel);
        }*/
    }
}