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

        public IQueryable<Ingredient> GetAll()
        {
            var ingredients = this.dbContext.Ingredients;

            return ingredients;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var ingredient = await this.dbContext.Ingredients
                .Where(i => i.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return ingredient;
        }

        public IQueryable<IngredientSimpleViewModel> GetAllToSimpleViewModel()
        {
            var ingredients = this.dbContext.Ingredients.To<IngredientSimpleViewModel>();

            return ingredients;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var ingredient = this.GetById(id);
            ingredient.IsDeleted = true;
            ingredient.DeletedOn = DateTime.UtcNow;

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public ICollection<IngredientSimpleViewModel> GetIngredientsByAllergenId(int allergenId)
        {
            var ingredients = this.dbContext.IngredientAllergen.Where(x => x.AllergenId == allergenId)
                .Select(x => x.Ingredient).To<IngredientSimpleViewModel>().ToList();

            return ingredients;
        }

        public IList<IngredientForRecipeViewModel> GetIngredientsVMByRecipeId(int recipeId)
        {
            var ingredients = this.dbContext.RecipeIngredient.Where(x => x.RecipeId == recipeId)
                .Select(x => x.Ingredient).To<IngredientForRecipeViewModel>().ToList();

            return ingredients;
        }

        public IList<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            var ingredients = this.dbContext.RecipeIngredient.Where(x => x.RecipeId == recipeId)
                .Select(x => x.Ingredient).ToList();

            return ingredients;
        }

        public IList<string> GetQuantitiesByRecipeId(int recipeId)
        {
            var quantities = this.dbContext.RecipeIngredient.Where(x => x.RecipeId == recipeId)
                .Select(x => x.Quantity).ToList();

            return quantities;
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

            if (model.ImagePath != null)
            {
                ingredientFromDb.ImagePath = model.ImagePath;
            }

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
            var ingredient = this.dbContext.Ingredients.FirstOrDefault(x => x.Id == id);

            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            return ingredient;
        }

    }
}
