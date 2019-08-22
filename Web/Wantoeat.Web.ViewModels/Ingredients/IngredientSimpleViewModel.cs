namespace Wantoeat.Web.ViewModels.Ingredients
{
    using AutoMapper;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class IngredientSimpleViewModel : IMapFrom<Ingredient>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<IngredientAllergen, IngredientSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.IngredientId))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Ingredient.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Ingredient.ImagePath));

            configuration
                .CreateMap<RecipeIngredient, IngredientSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.Ingredient.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Ingredient.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Ingredient.ImagePath));
        }
    }
}
