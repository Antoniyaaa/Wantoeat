namespace Wantoeat.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using Wantoeat.Services.Data;

    [Authorize]
    public class FavoritesController : BaseController
    {
        private readonly IFavoritesService favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
        }

        public async Task<IActionResult> All()
        {
            var favouriteRecipes = this.favoritesService.GetAllFavouritesByUserName(this.User.Identity.Name).ToList();

            return View(favouriteRecipes);
        }

        public async Task<IActionResult> Add(int id)
        {
            await this.favoritesService.AddAsync(id, this.User.Identity.Name);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.favoritesService.DeleteAsync(id, this.User.Identity.Name);

            return RedirectToAction(nameof(All));
        }
    }
}