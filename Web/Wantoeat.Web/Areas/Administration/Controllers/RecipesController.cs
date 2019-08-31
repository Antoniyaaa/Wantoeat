namespace Wantoeat.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Wantoeat.Services;
    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Ingredients;
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipesController : AdministrationController
    {
        private readonly IIngredientService ingredientService;
        private readonly ICookingTimeService cookingTimeService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;
        private readonly IRecipeService recipeService;

        public RecipesController(IIngredientService ingredientService, ICookingTimeService cookingTimeService,
            ICategoryService categoryService, IImageService imageService, IRecipeService recipeService)
        {
            this.ingredientService = ingredientService;
            this.cookingTimeService = cookingTimeService;
            this.categoryService = categoryService;
            this.imageService = imageService;
            this.recipeService = recipeService;
        }

        public async Task<IActionResult> Create()
        {
            var cookingTimes = await this.cookingTimeService.AllToSelectListItems();
            var categories = await this.categoryService.AllToSelectListItems();

            this.ViewData["ingredients"] = await this.ingredientService.GetAllToViewModel<RecipeCreateIngredientViewModel>().ToListAsync();

            RecipeCreateInputModel model = new RecipeCreateInputModel
            {
                Categories = categories,
                CookingTimes = cookingTimes,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var cookingTimes = await this.cookingTimeService.AllToSelectListItems();
                var categories = await this.categoryService.AllToSelectListItems();

                this.ViewData["ingredients"] = await this.ingredientService.GetAllToViewModel<RecipeCreateIngredientViewModel>().ToListAsync();

                model.Categories = categories;
                model.CookingTimes = cookingTimes;

                return this.View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length != 0)
            {
                model.ImagePath = this.imageService.UploadImage(model.ImageFile, model.Name);
            }

            var recipe = await this.recipeService.CreateAsync(model);

            if (recipe == null)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("Details", new { id = recipe.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.recipeService.GetViewModelByIdAsync<RecipeEditInputModel>(id);

            if (model == null)
            {
                throw new NullReferenceException();
            }

            var unusedIngredients = await this.ingredientService.GetAllUnused(id).ToListAsync();

            this.ViewData["ingredients"] = unusedIngredients.Select(ingredient => new RecipeCreateIngredientViewModel
            {
                Name = ingredient.Name

            }).OrderBy(x => x.Name)
            .ToList();

            var allCookingTimes = await this.cookingTimeService.GetAll().ToListAsync();
            this.ViewData["cookingTimes"] = allCookingTimes.Select(item => item.Name).ToList();

            var allCategories = await this.categoryService.GetAll().ToListAsync();
            this.ViewData["categories"] = allCategories.Select(item => item.Name).ToList();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RecipeEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                var unusedIngredients = await this.ingredientService.GetAllUnused(model.Id).ToListAsync();

                this.ViewData["ingredients"] = unusedIngredients.Select(ingredient => new RecipeCreateIngredientViewModel
                {
                    Name = ingredient.Name

                }).OrderBy(x => x.Name)
                .ToList();

                var allCookingTimes = await this.cookingTimeService.GetAll().ToListAsync();
                this.ViewData["cookingTimes"] = allCookingTimes.Select(item => item.Name).ToList();

                var allCategories = await this.categoryService.GetAll().ToListAsync();
                this.ViewData["categories"] = allCategories.Select(item => item.Name).ToList();

                return this.View(model);
            }

            if (model.NewImageFile != null && model.NewImageFile.Length != 0)
            {
                if (model.ImagePath != null)
                {
                    model.ImagePath = this.imageService.ReplaceImage(model.NewImageFile, model.ImagePath, model.Name);
                }
                else
                {
                    model.ImagePath = this.imageService.UploadImage(model.NewImageFile, model.Name);
                }
            }

            var recipe = await this.recipeService.EditAsync(model);

            if (recipe == null)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("Details", new { id = recipe.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await this.recipeService.GetViewModelByIdAsync<RecipeSimpleViewModel>(id);

            if (viewModel == null)
            {
                throw new NullReferenceException();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RecipeSimpleViewModel viewModel)
        {
            var result = await this.recipeService.DeleteByIdAsync(viewModel.Id);

            if (result == false)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("All");
        }
    }
}