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

        IQueryable<IngredientSimpleViewModel> GetAllToSimpleViewModel();

        IQueryable<Ingredient> GetAll();

        IList<Ingredient> GetIngredientsByRecipeId(int recipeId);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        Task<bool> DeleteByIdAsync(int id);

        // Do I need this?
        ICollection<IngredientSimpleViewModel> GetIngredientsByAllergenId(int allergenId);

        IList<IngredientForRecipeViewModel> GetIngredientsVMByRecipeId(int recipeId);

        IList<string> GetQuantitiesByRecipeId(int recipeId);
    }
}
