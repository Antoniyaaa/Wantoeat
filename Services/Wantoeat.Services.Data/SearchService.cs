using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wantoeat.Data;
using Wantoeat.Data.Models;

namespace Wantoeat.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationDbContext dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Recipe> GetRecipesByIngredients(int[] ingredientIds)
        {
            var recipes = this.dbContext.RecipeIngredient
                .Where(x => ingredientIds.Contains(x.IngredientId))
                .GroupBy(x => x.Recipe).Select(y => new { Value = y.Key, Count = y.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Value);
            //.ToArray();

            return recipes;
        }
    }
}
