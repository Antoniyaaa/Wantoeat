namespace Wantoeat.Web.ViewModels.Allergens
{
    using AutoMapper;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class AllergenViewModel : IMapFrom<RecipeAllergen>, IMapTo<Allergen>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Allergen, AllergenSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Name));

            configuration
               .CreateMap<RecipeAllergen, AllergenSimpleViewModel>()
               .ForMember(x => x.Id, opts => opts.MapFrom(y => y.AllergenId))
               .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Allergen.Name));
        }

    }
}
