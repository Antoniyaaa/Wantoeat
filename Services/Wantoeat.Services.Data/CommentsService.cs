namespace Wantoeat.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Common.Repositories;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Comments;
    using Wantoeat.Web.ViewModels.Recipes;

    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRecipeService recipeService;

        public CommentsService(ApplicationDbContext dbContext, IRecipeService recipeService)
        {
            this.dbContext = dbContext;
            this.recipeService = recipeService;
        }

        public async Task<bool> AddAsync(CommentInputModel inputModel)
        {
            var recipe = await this.recipeService.GetViewModelByIdAsync<CommentCreateRecipeViewModel>(inputModel.RecipeId);

            if (recipe == null)
            {
                throw new NullReferenceException();
            }

            var comment = AutoMapper.Mapper.Map<Comment>(inputModel);

            this.dbContext.Comments.Add(comment);

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

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
