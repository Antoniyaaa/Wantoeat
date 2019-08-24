namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;
    using Wantoeat.Web.ViewModels.Ingredients;

    public interface IIngredientService
    {
        Task<Ingredient> CreateAsync(IngredientCreateInputModel model);

        Task<Ingredient> EditAsync(IngredientEditInputModel model);

        IQueryable<TViewModel> GetAllToViewModel<TViewModel>();

        IQueryable<TViewModel> GetAllToViewModelByIds<TViewModel>(int[] ids);

        IQueryable<Ingredient> GetAllUnused(int recipeId);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        Task<bool> DeleteByIdAsync(int id);
    }
}
