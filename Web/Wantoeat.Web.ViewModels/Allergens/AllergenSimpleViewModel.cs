namespace Wantoeat.Web.ViewModels.Allergens
{
    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class AllergenSimpleViewModel : IMapFrom<Allergen>, IMapTo<Allergen>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<IngredientAllergen, AllergenSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.AllergenId))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Allergen.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Allergen.ImagePath));

            configuration
                .CreateMap<RecipeAllergen, AllergenSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.AllergenId))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Allergen.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Allergen.ImagePath))
                .ReverseMap();
        }
    }
}