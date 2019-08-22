namespace Wantoeat.Web.Areas.Administration.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Recipes;

    public class RecipeEditViewModel : IMapFrom<Recipe>
    {
        public RecipeDetailViewModel Recipe { get; set; }

        [Display(Name = "Choose New Category")]
        public int CategoryId { get; set; }
        public ICollection<SelectListItem> Categories { get; set; }

        [Display(Name = "Choose New Cooking time")]
        public int CookingTimeId { get; set; }
        public ICollection<SelectListItem> CookingTimes { get; set; }

        [Display(Name = "Old Ingredients")]
        public ICollection<int> OldIngredientIds { get; set; }
        public ICollection<SelectListItem> OldIngredients { get; set; }

        public IList<string> OldQuantities { get; set; }

        [Display(Name = "Add Ingredients")]
        public ICollection<int> NewIngredientIds { get; set; }
        public ICollection<SelectListItem> NewIngredients { get; set; }

        [Display(Name = "Add Ingredients")]
        public ICollection<int> NewAddedIngredientIds { get; set; }
        public ICollection<SelectListItem> NewAddedIngredients { get; set; }

        public string NewImagePath { get; set; }

        [Display(Name = "New Image")]
        public IFormFile NewImageFile { get; set; }

    }
}
