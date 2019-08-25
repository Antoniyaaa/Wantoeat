namespace Wantoeat.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Data;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext dbContext;

        public RecipeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Recipe> CreateAsync(RecipeCreateInputModel model)
        {
            var recipe = AutoMapper.Mapper.Map<Recipe>(model);

            // TODO Mapping
            if (model.IngredientNames != null)
            {
                for (int i = 0; i < model.IngredientNames.Count(); i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        Recipe = recipe,
                        Ingredient = this.dbContext.Ingredients.FirstOrDefault(x => x.Name == model.IngredientNames[i]),
                        Quantity = model.RecipeIngredientQuantity[i],
                    };

                    if (this.dbContext.IngredientAllergen.Any(x => x.Ingredient.Name == model.IngredientNames[i] &&
                        !recipe.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                    {
                        RecipeAllergen recipeAllergen = new RecipeAllergen
                        {
                            Recipe = recipe,
                            Allergen = this.dbContext.IngredientAllergen.Where(x => x.Ingredient.Name == model.IngredientNames[i]).Select(x => x.Allergen).FirstOrDefault()
                        };
                        recipeAllergen = this.dbContext.RecipeAllergen.Add(recipeAllergen).Entity;
                        recipe.RecipeAllergens.Add(recipeAllergen);
                    }

                    recipeIngredient = this.dbContext.RecipeIngredient.Add(recipeIngredient).Entity;
                    recipe.RecipeIngredient.Add(recipeIngredient);
                }
            }

            recipe = this.dbContext.Recipes.Add(recipe).Entity;

            await this.dbContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> EditAsync(RecipeEditInputModel model)
        {
            // TODO Mapping
            var recipeFromDb = GetById(model.Id);

            recipeFromDb.Name = model.Name;
            recipeFromDb.Description = model.Description;
            recipeFromDb.Category = this.dbContext.Categories.Where(x => x.Name == model.CategoryName).FirstOrDefault();
            recipeFromDb.CookingTime = this.dbContext.CookingTimes.Where(x => x.Name == model.CookingTimeName).FirstOrDefault();
            recipeFromDb.ImagePath = model.ImagePath;
            recipeFromDb.ModifiedOn = DateTime.UtcNow;

            int counter = 0;

            foreach (var item in recipeFromDb.RecipeIngredient)
            {
                if (model.Quantity[counter] == null)
                {
                    this.dbContext.RecipeIngredient.Remove(item);
                }
                else
                {
                    item.Quantity = model.Quantity[counter];
                }

                counter++;
            }

            for (int i = recipeFromDb.RecipeIngredient.Count(); i < model.IngredientNames.Count(); i++)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipeFromDb,
                    Ingredient = this.dbContext.Ingredients.FirstOrDefault(x => x.Name == model.IngredientNames[i]),
                    Quantity = model.Quantity[i],
                };

                if (this.dbContext.IngredientAllergen.Any(x => x.Ingredient.Name == model.IngredientNames[i] &&
                    !recipeFromDb.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                {
                    RecipeAllergen recipeAllergen = new RecipeAllergen
                    {
                        Recipe = recipeFromDb,
                        Allergen = this.dbContext.IngredientAllergen.Where(x => x.Ingredient.Name == model.IngredientNames[i]).Select(x => x.Allergen).FirstOrDefault()
                    };

                    recipeAllergen = this.dbContext.RecipeAllergen.Add(recipeAllergen).Entity;
                    recipeFromDb.RecipeAllergens.Add(recipeAllergen);
                }

                recipeIngredient = this.dbContext.RecipeIngredient.Add(recipeIngredient).Entity;
                recipeFromDb.RecipeIngredient.Add(recipeIngredient);
            }

            await this.dbContext.SaveChangesAsync();

            return recipeFromDb;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var recipe = this.GetById(id);

            recipe.IsDeleted = true;
            recipe.DeletedOn = DateTime.UtcNow;

            int result = await this.dbContext.SaveChangesAsync();

            return result > 0;
        }

        public List<RecipesInGroupsViewModel<string, List<RecipeSimpleWithCategoryViewModel>>> GetGroupsByCategories()
        {
            var recipes = this.dbContext.Recipes.To<RecipeSimpleWithCategoryViewModel>();

            var recipesCategories = recipes.GroupBy(x => x.CategoryName)
                                    .Select(g => 
                                    new RecipesInGroupsViewModel<string, List<RecipeSimpleWithCategoryViewModel>>
                                    { GroupName = g.Key, Recipes = g.ToList() })
                                    .ToList();

            return recipesCategories;
        }

        public Recipe GetById(int id)
        {
            var recipe = this.dbContext.Recipes.Include(x => x.RecipeIngredient)
                .Include(x => x.RecipeAllergens)
                .Where(x => x.Id == id).FirstOrDefault();

            return recipe;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var recipe = await this.dbContext.Recipes
                .Where(r => r.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return recipe;
        }

        public IQueryable<Recipe> GetRecipesByIngredients(int[] ingredientIds)
        {
            var recipes = this.dbContext.RecipeIngredient
                .Where(x => ingredientIds.Contains(x.IngredientId))
                .GroupBy(x => x.Recipe).Select(y => new { Value = y.Key, Count = y.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Value);

            return recipes;
        }

        public IQueryable<TViewModel> GetAllToViewModel<TViewModel>()
        {
            var recipes = this.dbContext.Recipes.To<TViewModel>();

            return recipes;
        }

        public IQueryable<Recipe> GetByCategory(string criteria = null)
        {

            if (criteria != null && criteria != "None")
            {
                return this.dbContext.Recipes.Where(x => x.Category.Name == criteria);
            }

            return this.dbContext.Recipes;
        }

        public IQueryable<Recipe> GetAllNonContainingAllergen(int[] allergenIds)
        {
            /*var recipes = this.dbContext.RecipeAllergen
                //.Where(x => !allergenIds.Contains(x.AllergenId))
                .Select(x => x.Recipe)
                .Distinct().ToList();*/

            /*var ingredients = this.dbContext.Ingredients;
            var ingredientsRecipe = this.dbContext.RecipeIngredient
                                    .Where(x => x.RecipeId == recipeId)
                                    .Select(x => x.Ingredient);
            var result = ingredients.Where(x => !ingredientsRecipe.Any(x2 => x2.Id == x.Id));*/

            var allergens = this.dbContext.RecipeAllergen
                .Where(x => allergenIds.Contains(x.AllergenId))
                .Select(x => x.Recipe)
                .ToList();
            var recipes = this.dbContext.Recipes.ToList();
            var blq = recipes.Where(x => !allergens.Any(y => y.Id == x.Id)).AsQueryable();


            return blq;
        }
    }
}
