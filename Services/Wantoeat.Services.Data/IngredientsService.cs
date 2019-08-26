namespace Wantoeat.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class IngredientsService : IIngredientService
    {
        private readonly ApplicationDbContext dbContext;

        public IngredientsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Ingredient> GetAllUnused(int recipeId)
        {
            // TODO Refractor
            var ingredients = this.dbContext.Ingredients;
            var ingredientsRecipe = this.dbContext.RecipeIngredient
                                    .Where(x => x.RecipeId == recipeId)
                                    .Select(x => x.Ingredient);
            var result = ingredients.Where(x => !ingredientsRecipe.Any(x2 => x2.Id == x.Id));

            return result;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var ingredient = await this.dbContext.Ingredients
                .Where(i => i.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return ingredient;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var ingredient = this.dbContext.Ingredients
                            .FirstOrDefault(x => x.Id == id);

             if (ingredient == null)
             {
                 return false;
             }

            ingredient.IsDeleted = true;
            ingredient.DeletedOn = DateTime.UtcNow;

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Ingredient> CreateAsync(IngredientCreateInputModel model)
        {
            var ingredient = AutoMapper.Mapper.Map<Ingredient>(model);

            if (model.AllergenIds != null)
            {
                foreach (var item in model.AllergenIds)
                {
                    var allergen = this.dbContext.Allergens.FirstOrDefault(x => x.Id == item);

                    if (allergen == null)
                    {
                        continue;
                    }

                    IngredientAllergen ingredientAllergen = new IngredientAllergen
                    {
                        IngredientId = ingredient.Id,
                        AllergenId = item,
                        Allergen = allergen,
                    };

                    ingredientAllergen = this.dbContext.IngredientAllergen.Add(ingredientAllergen).Entity;

                    ingredient.IngredientAllergens.Add(ingredientAllergen);
                }
            }

            ingredient = this.dbContext.Ingredients.Add(ingredient).Entity;

            await this.dbContext.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> EditAsync(IngredientEditInputModel model)
        {
            var ingredientFromDb = this.dbContext.Ingredients
                .Include(x => x.IngredientAllergens)
                .FirstOrDefault(x => x.Id == model.Id);

            if (ingredientFromDb == null)
            {
                return null;
            }

            ingredientFromDb.Name = model.Name;
            ingredientFromDb.Description = model.Description;
            ingredientFromDb.ImagePath = model.ImagePath;
            ingredientFromDb.ModifiedOn = DateTime.UtcNow;

            if (model.AllergenNames != null)
            {
                var oldallergens = this.dbContext.IngredientAllergen
                    .Include(x => x.Allergen)
                    .Where(x => x.IngredientId == ingredientFromDb.Id)
                    .ToList();

                foreach (var item in model.AllergenNames)
                {
                    if (item == "RemoveOlds")
                    {
                        this.dbContext.RemoveRange();
                        continue;
                    }

                    var allergen = this.dbContext.Allergens.FirstOrDefault(x => x.Name == item);

                    if (allergen == null)
                    {
                        continue;
                    }

                    IngredientAllergen ingredientAllergen = new IngredientAllergen
                    {
                        Ingredient = ingredientFromDb,
                        Allergen = allergen,
                    };

                    if (oldallergens.Select(x => x.Allergen).Contains(allergen))
                    {
                        continue;
                    }

                    ingredientAllergen = this.dbContext.IngredientAllergen.Add(ingredientAllergen).Entity;
                }
            }

            await this.dbContext.SaveChangesAsync();

            return ingredientFromDb;
        }

        public IQueryable<TViewModel> GetAllToViewModel<TViewModel>()
        {
            var ingredients = this.dbContext.Ingredients.To<TViewModel>();

            return ingredients;
        }

        public IQueryable<TViewModel> GetAllToViewModelByIds<TViewModel>(int[] ids)
        {
            var ingredients = this.dbContext.Ingredients
                .Where(x => ids.Contains(x.Id))
                .To<TViewModel>();

            return ingredients;
        }
    }
}
