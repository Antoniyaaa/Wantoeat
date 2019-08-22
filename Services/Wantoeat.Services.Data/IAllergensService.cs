namespace Wantoeat.Services.Data
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;
    using Wantoeat.Web.ViewModels.Allergens;

    public interface IAllergensService
    {
        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        IQueryable<AllergenSimpleViewModel> GetAllToSimpleViewModel();

        IQueryable<Allergen> GetAll();

        Task<List<SelectListItem>> AllToSelectListItems();

        ICollection<AllergenViewModel> GetAllergensByIngredientId(int ingredientId);

        ICollection<AllergenViewModel> GetAllergensByRecipeId(int recipeId);
    }
}
