namespace Wantoeat.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Wantoeat.Services;
    using Wantoeat.Services.Data;
    using Wantoeat.Web.ViewModels.Allergens;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class IngredientsController : AdministrationController
    {
        private readonly IAllergensService allergensService;
        private readonly IIngredientService ingredientService;
        private readonly IImageService imageService;

        public IngredientsController(IAllergensService allergensService, IIngredientService ingredientService,
            IImageService imageService)
        {
            this.allergensService = allergensService;
            this.ingredientService = ingredientService;
            this.imageService = imageService;
        }

        public async Task<IActionResult> Create()
        {
            var allergens = this.allergensService.AllToSelectListItems().ToList();

            var model = new IngredientCreateInputModel { Allergens = allergens };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IngredientCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.AllergenIds == null)
                {
                    var allergens = this.allergensService.AllToSelectListItems().ToList();
                    model.Allergens = allergens;
                }

                return this.View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length != 0)
            {
                model.ImagePath = this.imageService.UploadImage(model.ImageFile, model.Name);
            }

            var ingredient = await this.ingredientService.CreateAsync(model);

            if (ingredient == null)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("Details", new { id = ingredient.Id });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.ingredientService.GetViewModelByIdAsync<IngredientEditInputModel>(id);

            if (viewModel == null)
            {
                throw new NullReferenceException();
            }

            this.ViewData["allergens"] = this.allergensService.GetAllToViewModel<IngredientCreateAllergenViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IngredientEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ViewData["allergens"] = this.allergensService.GetAllToViewModel<IngredientCreateAllergenViewModel>();

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

            var ingredient = await this.ingredientService.EditAsync(model);

            if (ingredient == null)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("Details", new { id = ingredient.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await this.ingredientService.GetViewModelByIdAsync<IngredientSimpleViewModel>(id);

            if (viewModel == null)
            {
                throw new NullReferenceException();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IngredientSimpleViewModel viewModel)
        {
            var result = await this.ingredientService.DeleteByIdAsync(viewModel.Id);

            if (result == false)
            {
                throw new NullReferenceException();
            }

            return this.RedirectToAction("All");
        }
    }
}