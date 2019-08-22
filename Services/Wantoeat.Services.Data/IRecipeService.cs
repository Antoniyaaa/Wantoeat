using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wantoeat.Data.Models;
using Wantoeat.Web.ViewModels.Ingredients;
using Wantoeat.Web.ViewModels.Recipes;

namespace Wantoeat.Services.Data
{
    public interface IRecipeService
    {
        Recipe Create(string name, string description, int cookingTimeId, int categoryId, IList<int> ingredients, string imagePath);
        // Recipe AddIngredients(int id, string description, IEnumerable<string> quantities);

        Recipe Edit(int id, string name, string description, int cookingTimeId, int categoryId, IList<int> ingredients, string imagePath);

        IQueryable<RecipeSimpleViewModel> GetAllToSimpleViewModel();

        Recipe GetById(int id);

        Task DeleteByIdAsync(int id);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        IQueryable<Recipe> All();
    }
}
