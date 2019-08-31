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
            if (model.CategoryId == 0 || model.CookingTimeId == 0)
            {
                return null;
            }

            var recipe = AutoMapper.Mapper.Map<Recipe>(model);

            if (model.IngredientQuantities != null)
            {
                if (model.IngredientQuantities.IngredientNames.Count() != model.IngredientQuantities.RecipeIngredientQuantity.Count())
                {
                    throw new ArgumentNullException();
                }

                for (int i = 0; i < model.IngredientQuantities.IngredientNames.Count(); i++)
                {
                    var ingredient = this.dbContext.Ingredients.FirstOrDefault(x => x.Name == model.IngredientQuantities.IngredientNames[i]);

                    if (ingredient == null)
                    {
                        return null;
                    }

                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        Recipe = recipe,
                        Ingredient = ingredient,
                        Quantity = model.IngredientQuantities.RecipeIngredientQuantity[i],
                    };

                    if (this.dbContext.IngredientAllergen.Any(x => x.Ingredient.Name == ingredient.Name &&
                        !recipe.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                    {
                        RecipeAllergen recipeAllergen = new RecipeAllergen
                        {
                            Recipe = recipe,
                            Allergen = this.dbContext.IngredientAllergen.Where(x => x.Ingredient.Name == model.IngredientQuantities.IngredientNames[i]).Select(x => x.Allergen).FirstOrDefault()
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
            var recipeFromDb = GetById(model.Id);

            recipeFromDb.Name = model.Name;
            recipeFromDb.Description = model.Description;
            recipeFromDb.Category = this.dbContext.Categories.Where(x => x.Name == model.CategoryName).FirstOrDefault();
            recipeFromDb.CookingTime = this.dbContext.CookingTimes.Where(x => x.Name == model.CookingTimeName).FirstOrDefault();
            recipeFromDb.ImagePath = model.ImagePath;
            recipeFromDb.ModifiedOn = DateTime.UtcNow;

            int counter = 0;

            foreach (var currentIngredient in recipeFromDb.RecipeIngredient)
            {
                if (model.Quantity[counter] == null)
                {
                    this.dbContext.RecipeIngredient.Remove(currentIngredient);
                }
                else
                {
                    currentIngredient.Quantity = model.Quantity[counter];
                }

                counter++;
            }

            if (model.IngredientQuantities != null)
            {
                if (model.IngredientQuantities.IngredientNames.Count() != model.IngredientQuantities.RecipeIngredientQuantity.Count())
                {
                    throw new ArgumentNullException();
                }

                for (int i = 0; i < model.IngredientQuantities.IngredientNames.Count(); i++)
                {
                    var ingredient = this.dbContext.Ingredients.FirstOrDefault(x => x.Name == model.IngredientQuantities.IngredientNames[i]);

                    if (ingredient == null)
                    {
                        return null;
                    }

                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        Recipe = recipeFromDb,
                        Ingredient = ingredient,
                        Quantity = model.IngredientQuantities.RecipeIngredientQuantity[i],
                    };

                    if (this.dbContext.IngredientAllergen.Any(x => x.Ingredient.Name == ingredient.Name &&
                        !recipeFromDb.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                    {
                        RecipeAllergen recipeAllergen = new RecipeAllergen
                        {
                            Recipe = recipeFromDb,
                            Allergen = this.dbContext.IngredientAllergen.Where(x => x.Ingredient.Name == model.IngredientQuantities.IngredientNames[i]).Select(x => x.Allergen).FirstOrDefault()
                        };

                        recipeAllergen = this.dbContext.RecipeAllergen.Add(recipeAllergen).Entity;
                        recipeFromDb.RecipeAllergens.Add(recipeAllergen);
                    }

                    recipeIngredient = this.dbContext.RecipeIngredient.Add(recipeIngredient).Entity;
                    recipeFromDb.RecipeIngredient.Add(recipeIngredient);
                }
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

            if (!this.dbContext.Categories.Any())
            {
                var recipesWhenNoCategory = new RecipesInGroupsViewModel<string, List<RecipeSimpleWithCategoryViewModel>>
                {
                    GroupName = "Common category",
                    Recipes = recipes.ToList()
                };

                var allRecipesInOneCategory = new List<RecipesInGroupsViewModel<string, List<RecipeSimpleWithCategoryViewModel>>>();
                allRecipesInOneCategory.Add(recipesWhenNoCategory);

                return allRecipesInOneCategory;
            }

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
                .Include(x => x.RecipeIngredient)
                .Where(r => r.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return recipe;
        }

        public IQueryable<Recipe> GetRecipesByMatchingIngredients(int[] ingredientIds)
        {
            var recipes = this.dbContext.RecipeIngredient
                .Where(x => ingredientIds.Contains(x.IngredientId))
                .GroupBy(x => x.Recipe).Select(y => new { Value = y.Key, Count = y.Count() })
                .OrderByDescending(x => x.Count)
                .Select(x => x.Value);

            // TODO Show the Count of Matches

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

        public IQueryable<Recipe> GetAllNonContainingByAllergenId(int[] allergenIds)
        {
            var recipesWithallergens = this.dbContext.RecipeAllergen
                .Where(x => allergenIds.Contains(x.AllergenId))
                .Select(x => x.Recipe)
                .ToList();

            var allRecipes = this.dbContext.Recipes.ToList();

            var result = allRecipes.Where(x => !recipesWithallergens.Any(y => y.Id == x.Id)).AsQueryable();

            return result;
        }
    }
}
