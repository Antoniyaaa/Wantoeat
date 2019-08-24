namespace Wantoeat.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipesController : BaseController
    {
        private readonly IRecipeService recipesService;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipesService = recipeService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.recipesService.GetViewModelByIdAsync<RecipeDetailViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var recipes = await this.recipesService.GetAllToViewModel<RecipeSimpleViewModel>().ToListAsync();

            return this.View(recipes);
        }

    }
}