namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.ValidationAttributes;

    public class RecipeCreateInputModel : IMapFrom<Recipe>, IMapTo<Recipe>
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name length must be between {1} and {0} symbols.", MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Choose Category")]
        public int CategoryId { get; set; }
        public ICollection<SelectListItem> Categories { get; set; }

        [Display(Name = "Choose Cooking time")]
        public int CookingTimeId { get; set; }
        public ICollection<SelectListItem> CookingTimes { get; set; }

        [Required]
        public IngredientQuantities IngredientQuantities { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Upload Image")]
        [ImageValidationAttribute(ErrorMessage = "Allowed extensions: jpg, jpeg, png, bmp.")]
        public IFormFile ImageFile { get; set; }
    }

    public class IngredientQuantities : IValidatableObject
    {
        [Display(Name = "Choose Ingredients")]
        public IList<string> IngredientNames { get; set; }

        [Display(Name = "Add quantity for each Ingredient")]
        public IList<string> RecipeIngredientQuantity { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.IngredientNames.Count != this.RecipeIngredientQuantity.Count)
            {
                yield return new ValidationResult("Ingredients count and quantities count must be equal.");
            }
        }
    }
}
