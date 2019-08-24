namespace Wantoeat.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class IngredientsController : BaseController
    {
        private readonly IIngredientService ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.ingredientService.GetViewModelByIdAsync<IngredientDetailViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var ingredients = await this.ingredientService.GetAllToViewModel<IngredientSimpleViewModel>().ToListAsync();

            return this.View(ingredients);
        }
    }
}
