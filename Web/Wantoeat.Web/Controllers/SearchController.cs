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
    public class SearchController : BaseController
    {
        private readonly IIngredientService ingredientService;
        private readonly IRecipeService recipeService;
        private readonly ApplicationDbContext dbContext;

        public SearchController(IIngredientService ingredientService, IRecipeService recipeService
            , ApplicationDbContext dbContext)
        {
            this.ingredientService = ingredientService;
            this.recipeService = recipeService;
            this.dbContext = dbContext;
        }

        /* public IActionResult Index(string searchString, string recipeCategory)
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
         }*/

    }
}