namespace Wantoeat.Web.ViewModels.Ingredients
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.ValidationAttributes;

    public class IngredientCreateInputModel : IMapTo<Ingredient>
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name length must be between {1} and {0} symbols.", MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<int> AllergenIds { get; set; }

        [Display(Name = "Allergens")]
        public ICollection<SelectListItem> Allergens { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Upload Image")]
        [ImageValidationAttribute(ErrorMessage = "Allowed extensions: jpg, jpeg, png, bmp.")]
        public IFormFile ImageFile { get; set; }
    }
}
