namespace Wantoeat.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Wantoeat.Services.Data;
    using Wantoeat.Services.Mapping;
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

        public async Task<IActionResult> All([FromQuery]string category = null)
        {
            var recipes = this.recipesService.GetByCategory(category).To<RecipeSimpleViewModel>().ToList();

            this.ViewData["category"] = category;

            return this.View(recipes);
        }


    }
}