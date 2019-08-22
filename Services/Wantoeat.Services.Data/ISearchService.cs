using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wantoeat.Data.Models;

namespace Wantoeat.Services.Data
{
    public interface ISearchService
    {
        IQueryable<Recipe> GetRecipesByIngredients(int[] ingredientIds);
    }
}
