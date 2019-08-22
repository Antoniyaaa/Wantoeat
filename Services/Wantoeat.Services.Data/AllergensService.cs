namespace Wantoeat.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;

    public class AllergensService : IAllergensService
    {
        private readonly ApplicationDbContext dbContext;

        public AllergensService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SelectListItem>> AllToSelectListItems()
        {
            var allergens = await this.dbContext.Allergens
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name,
                                })
                                .ToListAsync();

            return allergens;
        }

        public IQueryable<Allergen> GetAll()
        {
            return this.dbContext.Allergens;
        }

        public ICollection<AllergenViewModel> GetAllergensByIngredientId(int ingredientId)
        {
            var allergens = this.dbContext.IngredientAllergen.Where(x => x.IngredientId == ingredientId)
                .Select(x => x.Allergen).To<AllergenViewModel>().ToList();

            return allergens;
        }

        public ICollection<AllergenViewModel> GetAllergensByRecipeId(int recipeId)
        {
            var allergens = this.dbContext.RecipeAllergen.Where(x => x.RecipeId == recipeId)
                .Select(x => x.Allergen).To<AllergenViewModel>().ToList();

            return allergens;
        }

        public IQueryable<AllergenSimpleViewModel> GetAllToSimpleViewModel()
        {
            var allergens = this.dbContext.Allergens.To<AllergenSimpleViewModel>();

            return allergens;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var allergen = await this.dbContext.Allergens
                .Where(i => i.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return allergen;
        }
    }
}
