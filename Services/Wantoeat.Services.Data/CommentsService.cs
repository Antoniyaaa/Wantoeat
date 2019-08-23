namespace Wantoeat.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommentsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> Add(CommentInputModel inputModel)
        {
            var comment = AutoMapper.Mapper.Map<Comment>(inputModel);

            comment = this.dbContext.Comments.Add(comment).Entity;

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        // TODO Through repository!

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var comment = await this.dbContext.Comments
                .Where(r => r.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return comment;
        }


        public async Task<bool> DeleteByIdAsync(int id)
        {
            var comment = this.dbContext.Comments.Where(x => x.Id == id).FirstOrDefault();

            comment.IsDeleted = true;
            comment.DeletedOn = DateTime.UtcNow;

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
