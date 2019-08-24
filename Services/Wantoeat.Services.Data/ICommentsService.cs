namespace Wantoeat.Services.Data
{
    using System.Threading.Tasks;

    using Wantoeat.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<bool> AddAsync(CommentInputModel inputModel);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        Task<bool> DeleteByIdAsync(int id);
    }
}
