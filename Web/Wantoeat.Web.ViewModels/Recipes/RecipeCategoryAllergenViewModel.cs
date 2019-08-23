namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;

    public class RecipeCategoryAllergenViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public List<AllergenViewModel> RecipeAllergens { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Recipe, RecipeCategoryAllergenViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.ImagePath))
                .ForMember(x => x.CategoryName, opts => opts.MapFrom(y => y.Category.Name));
        }
    }
}
