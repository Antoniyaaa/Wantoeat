namespace Wantoeat.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Wantoeat.Services.Data;

    public class CategoriesController : BaseController
    {
        private readonly IRecipeService recipeService;

        public CategoriesController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        public IActionResult AllRecipesByCategory()
        {
            var recipes = this.recipeService.GetGroupsByCategories();

            return this.View(recipes);
        }
    }
}