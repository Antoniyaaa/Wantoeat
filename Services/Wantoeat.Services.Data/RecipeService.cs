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
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext dbContext;

        public RecipeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Recipe> All()
        {
            var recipes = this.dbContext.Recipes;

            return recipes;
        }

        public Recipe Create(string name, string description, int cookingTimeId, int categoryId, IList<int> ingredients, string imagePath)
        {
            Recipe recipe = new Recipe
            {
                Name = name,
                Description = description,
                CookingTimeId = cookingTimeId,
                CategoryId = categoryId,
                ImagePath = imagePath,
            };

            if (ingredients != null)
            {
                for (int i = 0; i < ingredients.Count(); i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ingredients[i],
                        //Quantity = quantities[i],
                    };

                    if (this.dbContext.IngredientAllergen.Any(x => x.IngredientId == ingredients[i] &&
                        !recipe.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                    {
                        RecipeAllergen recipeAllergen = new RecipeAllergen
                        {
                            RecipeId = recipe.Id,
                            AllergenId = this.dbContext.IngredientAllergen.Where(x => x.IngredientId == ingredients[i]).Select(x => x.AllergenId).FirstOrDefault()
                        };
                        recipeAllergen = this.dbContext.RecipeAllergen.Add(recipeAllergen).Entity;
                        recipe.RecipeAllergens.Add(recipeAllergen);
                    }

                    recipeIngredient = this.dbContext.RecipeIngredient.Add(recipeIngredient).Entity;
                    recipe.RecipeIngredient.Add(recipeIngredient);
                }
            }

            recipe = this.dbContext.Recipes.Add(recipe).Entity;

            this.dbContext.SaveChanges();

            return recipe;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var recipe = this.GetById(id);
            recipe.IsDeleted = true;
            recipe.DeletedOn = DateTime.UtcNow;

            // TODO Go through Repository, so asyn make sence
            this.dbContext.SaveChanges();
        }

        public Recipe Edit(int id, string name, string description, int cookingTimeId, int categoryId, IList<int> ingredients, string imagePath)
        {
            var recipe = this.GetById(id);

            recipe.Name = name;
            recipe.Description = description;

            if (cookingTimeId != 0)
            {
                recipe.CookingTimeId = cookingTimeId;
            }

            if (categoryId != 0)
            {
                recipe.CategoryId = categoryId;
            }

            if (imagePath != null)
            {
                recipe.ImagePath = imagePath;
            }

            if (ingredients != null)
            {
                var oldIngredients = this.dbContext.RecipeIngredient.Where(x => x.RecipeId == id).ToList();
                var oldAllergens = this.dbContext.RecipeAllergen.Where(x => x.RecipeId == id).ToList();

                this.dbContext.RemoveRange(oldIngredients);
                this.dbContext.RemoveRange(oldAllergens);


                for (int i = 0; i < ingredients.Count(); i++)
                {
                    RecipeIngredient recipeIngredient = new RecipeIngredient
                    {
                        RecipeId = recipe.Id,
                        IngredientId = ingredients[i],
                    };

                    if (this.dbContext.IngredientAllergen.Any(x => x.IngredientId == ingredients[i] &&
                        !recipe.RecipeAllergens.Any(y => y.AllergenId == x.AllergenId)))
                    {
                        RecipeAllergen recipeAllergen = new RecipeAllergen
                        {
                            RecipeId = recipe.Id,
                            AllergenId = this.dbContext.IngredientAllergen.Where(x => x.IngredientId == ingredients[i]).Select(x => x.AllergenId).FirstOrDefault()
                        };
                        recipeAllergen = this.dbContext.RecipeAllergen.Add(recipeAllergen).Entity;
                        recipe.RecipeAllergens.Add(recipeAllergen);
                    }

                    recipeIngredient = this.dbContext.RecipeIngredient.Add(recipeIngredient).Entity;
                    recipe.RecipeIngredient.Add(recipeIngredient);
                }

            }

            this.dbContext.SaveChanges();

            return recipe;
        }

        public IQueryable<RecipeSimpleViewModel> GetAllToSimpleViewModel()
        {
            var recipes = this.dbContext.Recipes.To<RecipeSimpleViewModel>();

            return recipes;
        }

        public Recipe GetById(int id)
        {
            var recipe = this.dbContext.Recipes.Where(x => x.Id == id).FirstOrDefault();


            // TODO are the following g
            recipe.RecipeIngredient = this.dbContext.RecipeIngredient.Where(x => x.RecipeId == recipe.Id)
                .ToList();
            recipe.RecipeAllergens = this.dbContext.RecipeAllergen.Where(x => x.RecipeId == recipe.Id).ToList();


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
    }
}
