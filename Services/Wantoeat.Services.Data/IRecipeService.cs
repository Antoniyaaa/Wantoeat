﻿namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Wantoeat.Data.Models;
    using Wantoeat.Web.ViewModels.Recipes;

    public interface IRecipeService
    {
        Task<Recipe> CreateAsync(RecipeCreateInputModel model);

        Task<Recipe> EditAsync(RecipeEditInputModel model);

        IQueryable<TViewModel> GetAllToViewModel<TViewModel>();

        List<RecipesInGroupsViewModel<string, List<RecipeSimpleWithCategoryViewModel>>> GetGroupsByCategories();

        Recipe GetById(int id);

        Task<bool> DeleteByIdAsync(int id);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        IQueryable<Recipe> GetRecipesByMatchingIngredients(int[] ingredientIds);

        IQueryable<Recipe> GetByCategory(string criteria = null);

        IQueryable<Recipe> GetAllNonContainingByAllergenId(int[] allergenIds);
    }
}
