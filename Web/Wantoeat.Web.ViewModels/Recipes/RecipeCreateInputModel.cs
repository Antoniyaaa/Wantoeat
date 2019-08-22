using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wantoeat.Data.Models;
using Wantoeat.Services.Mapping;
using Wantoeat.Web.ViewModels.ValidationAttributes;

namespace Wantoeat.Web.ViewModels.Recipes
{
    public class RecipeCreateInputModel : IMapTo<Recipe>, IHaveCustomMappings
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

        public ICollection<string> RecipeIngredientName { get; set; }

        public ICollection<string> RecipeIngredientQuantity { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Upload Image")]
        [ImageValidationAttribute(ErrorMessage = "Allowed extensions: jpg, jpeg, png, bmp.")]
        public IFormFile ImageFile { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
        }
    }
}
