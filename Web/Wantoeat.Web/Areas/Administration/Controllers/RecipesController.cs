namespace Wantoeat.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Wantoeat.Services.Data;
    using Wantoeat.Services;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Recipes;
    using Microsoft.EntityFrameworkCore;
    using Wantoeat.Web.ViewModels.Ingredinets;

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

            var allIngredients = await this.ingredientService.GetAll().ToListAsync();

            this.ViewData["ingredients"] = allIngredients.Select(ingredient => new RecipeCreateIngredientViewModel
            {
                Name = ingredient.Name

            }).OrderBy(x => x.Name)
            .ToList();

            RecipeCreateInputModel viewModel = new RecipeCreateInputModel
            {
                Categories = categories,
                CookingTimes = cookingTimes,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecipeCreateInputModel viewModel)
        {
            /*if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (viewModel.ImageFile != null && viewModel.ImageFile.Length != 0)
            {
                viewModel.ImagePath = this.imageService.UploadImage(viewModel.ImageFile, viewModel.Name);
            }

            var recipe = this.recipeService.Create(viewModel.Name, viewModel.Description, viewModel.CookingTimeId, 
                 viewModel.CategoryId, viewModel.IngredientIds, viewModel.ImagePath);

             return this.RedirectToAction("Details", new { id = recipe.Id });
            return this.View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var recipe = this.recipeService.GetById(id);
            // TODO Extract method or similar or make Service...
            var ingredients = this.ingredientService.All();
            var ingredientsList = ingredients.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).OrderBy(x => x.Text)
                                              .ToList();

            var addedIngredients = this.ingredientService.GetIngredientsByRecipeId(id);
            var addedIngredientsList = addedIngredients.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).OrderBy(x => x.Text)
                                              .ToList();

            var unaddedIngredients = ingredients.Where(x => !addedIngredients.Any(y => y.Id == x.Id));
            var unAddedIngredientsList = unaddedIngredients.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).OrderBy(x => x.Text).ToList();


            var cookingTimes = this.cookingTimeService.All();
            var cookingTimesList = cookingTimes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            })
                                              .ToList();

            var categories = this.categoryService.All();
            var categoriesList = categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            })
                .OrderBy(x => x.Text)
                                              .ToList();

            // TODO Add Category Name and Cooking Time
            var viewModel = new RecipeEditViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Categories = categoriesList,
                CookingTimes = cookingTimesList,
                Ingredients = unAddedIngredientsList,
                AddedIngredients = addedIngredientsList,
                ImagePath = recipe.ImagePath,
            };

            return this.View(viewModel);*/
            return this.View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Edit(RecipeEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (viewModel.NewImageFile != null && viewModel.NewImageFile.Length != 0)
            {
                viewModel.ImagePath = this.imageService.ReplaceImage(viewModel.NewImageFile, viewModel.ImagePath, viewModel.Name);
            }

            var recipe = this.recipeService.Edit(viewModel.Id, viewModel.Name, viewModel.Description, viewModel.CookingTimeId,
                viewModel.CategoryId, viewModel.IngredientIds, viewModel.ImagePath);

            return this.RedirectToAction("Details", new { id = recipe.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await this.recipeService.GetViewModelByIdAsync<RecipeSimpleViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RecipeSimpleViewModel viewModel)
        {
            await this.recipeService.DeleteByIdAsync(viewModel.Id);

            return this.RedirectToAction("Details", new { id = 1 });
        }*/


            /*private List<SelectListItem> CreateSelectListItemsList<T>(List<T> items)
            {

            }*/
        }
    }