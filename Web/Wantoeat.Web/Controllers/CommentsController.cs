namespace Wantoeat.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Comments;
    using Wantoeat.Web.ViewModels.Recipes;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly IRecipeService recipeService;
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(IRecipeService recipeService, ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipeService = recipeService;
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Add(int id)
        {
            var recipe = await this.recipeService.GetViewModelByIdAsync<CommentCreateRecipeViewModel>(id);

            var inputModel = new CommentInputModel { Recipe = recipe, RecipeId = recipe.RecipeId};

            return this.View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentInputModel inputModel)
        {
            inputModel.UserId = this.userManager.GetUserId(this.HttpContext.User);

            await this.commentsService.Add(inputModel);

            return this.RedirectToAction("Details", "Recipes", new { id = inputModel.RecipeId });
        }
    }
}