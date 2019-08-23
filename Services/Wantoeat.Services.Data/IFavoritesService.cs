namespace Wantoeat.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Web.ViewModels.Recipes;

    public interface IFavoritesService
    {
        Task<bool> AddAsync(int id, string name);

        IQueryable<RecipeSimpleViewModel> GetAllFavouritesByUserName(string name);

        Task<bool> DeleteAsync(int id, string name);
    }
}
