namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.ValidationAttributes;

    public class RecipeEditInputModel : IMapFrom<Recipe>, IMapTo<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length must be between {1} and {0} symbols.", MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Cooking time")]
        public string CookingTimeName { get; set; }

        [Display(Name = "Current ingredients")]
        public IList<string> IngredientNames { get; set; }

        [Display(Name = "Current quantities")]
        public IList<string> Quantity { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Current image")]
        public string ImagePath { get; set; }

        public IngredientQuantities IngredientQuantities { get; set; }

        [Display(Name = "Upload new Image")]
        [ImageValidationAttribute(ErrorMessage = "Allowed extensions: jpg, jpeg, png, bmp.")]
        public IFormFile NewImageFile { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
              .CreateMap<Recipe, RecipeEditInputModel>()
              .ForMember(destination => destination.Quantity,
                          opts => opts.MapFrom(origin => origin.RecipeIngredient
                          .Select(z => z.Quantity)))
              .ForMember(destination => destination.IngredientNames,
                          opts => opts.MapFrom(origin => origin.RecipeIngredient
                          .Select(z => z.Ingredient.Name)));
        }
    }
}
