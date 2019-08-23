namespace Wantoeat.Web.ViewModels.Ingredients
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.ValidationAttributes;

    public class IngredientEditInputModel : IMapFrom<Ingredient>, IMapTo<Ingredient>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length must be between {1} and {0} symbols.", MinimumLength = 3)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Current allergens")]
        public ICollection<string> AllergenNames { get; set; }

        [Display(Name = "Current image")]
        public string ImagePath { get; set; }

        [Display(Name = "Upload new Image")]
        [ImageValidationAttribute(ErrorMessage = "Allowed extensions: jpg, jpeg, png, bmp.")]
        public IFormFile NewImageFile { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
               .CreateMap<Ingredient, IngredientEditInputModel>()
               .ForMember(destination => destination.AllergenNames,
                           opts => opts.MapFrom(origin => origin.IngredientAllergens
                           .Select(z => z.Allergen.Name)));
        }
    }
}
