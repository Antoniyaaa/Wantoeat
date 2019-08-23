namespace Wantoeat.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Allergens;

    public class AllergensController : BaseController
    {
        private readonly IAllergensService allergensService;

        public AllergensController(IAllergensService allergensService)
        {
            this.allergensService = allergensService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.allergensService.GetViewModelByIdAsync<AllergenDetailViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var allergens = await this.allergensService.GetAllToSimpleViewModel().ToListAsync();

            return this.View(allergens);
        }
    }
}