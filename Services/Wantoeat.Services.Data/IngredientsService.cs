namespace Wantoeat.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Data;
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
            var ingredient = this.GetById(id);
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
                    IngredientAllergen ingredientAllergen = new IngredientAllergen
                    {
                        IngredientId = ingredient.Id,
                        AllergenId = item,
                        Allergen = this.dbContext.Allergens.FirstOrDefault(x => x.Id == item),
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
            var ingredientFromDb = GetById(model.Id);

            ingredientFromDb.Name = model.Name;
            ingredientFromDb.Description = model.Description;
            ingredientFromDb.ImagePath = model.ImagePath;
            ingredientFromDb.ModifiedOn = DateTime.UtcNow;

            var oldallergens = this.dbContext.IngredientAllergen.Where(x => x.IngredientId == model.Id).ToList();

            if (oldallergens.Any() && model.AllergenNames == null)
            {
                this.dbContext.RemoveRange(oldallergens);
            }

            if (model.AllergenNames != null)
            {
                this.dbContext.RemoveRange(oldallergens);

                foreach (var item in model.AllergenNames)
                {
                    IngredientAllergen ingredientAllergen = new IngredientAllergen
                    {
                        Ingredient = ingredientFromDb,
                        Allergen = this.dbContext.Allergens.FirstOrDefault(x => x.Name == item),
                    };

                    ingredientAllergen = this.dbContext.IngredientAllergen.Add(ingredientAllergen).Entity;

                    ingredientFromDb.IngredientAllergens.Add(ingredientAllergen);
                }

            }

            await this.dbContext.SaveChangesAsync();

            return ingredientFromDb;
        }

        private Ingredient GetById(int id)
        {
            var ingredient = this.dbContext.Ingredients
                .Include(x => x.IngredientAllergens)
                .FirstOrDefault(x => x.Id == id);

            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            return ingredient;
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
