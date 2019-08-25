namespace Wantoeat.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Comments;

    public class CommentsController : AdministrationController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await this.commentsService.GetViewModelByIdAsync<CommentViewModel>(id);

            if (viewModel == null)
            {
                return View("Error");
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CommentViewModel viewModel)
        {
            var result = await this.commentsService.DeleteByIdAsync(viewModel.Id);

            if (result == false)
            {
                return View("Error");
            }

            return this.RedirectToAction("All", "Recipes");
        }
    }
}