namespace Wantoeat.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Recipes;

    public class FavoritesService : IFavoritesService
    {
        private readonly ApplicationDbContext dbContext;

        public FavoritesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Add(int id, string name)
        {
            var user = this.dbContext.Users.Include(x => x.FavouriteRecipes)
                        .FirstOrDefault(x => x.UserName == name);

            // TODO if already is there user.FavouriteRecipes.Any(x => x.RecipeId == id)

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var recipe = this.dbContext.Recipes.Where(x => x.Id == id).FirstOrDefault();

            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe));
            }

            var favourite = new ApplicationUserFavoriteRecipes
            {
                RecipeId = id,
                ApplicationUserId = user.Id
            };

            this.dbContext.ApplicationUserFavoriteRecipes.Add(favourite);

            int result = this.dbContext.SaveChanges();

            return result > 0;
        }

        public IQueryable<RecipeSimpleViewModel> GetAllFavouritesByUserName(string name)
        {
            var favoriteRecipes = this.dbContext.ApplicationUserFavoriteRecipes
                .Include(c => c.Recipe).ThenInclude(x => x.Comments)
                .Where(x => x.ApplicationUser.UserName == name).To<RecipeSimpleViewModel>();

            return favoriteRecipes;
        }

        public async Task<bool> DeleteAsync(int id, string name)
        {
            var favoriteRecipe = this.dbContext.ApplicationUserFavoriteRecipes
                                   .FirstOrDefault(x => x.ApplicationUser.UserName == name 
                                   && x.RecipeId == id);

            this.dbContext.ApplicationUserFavoriteRecipes.Remove(favoriteRecipe);

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
