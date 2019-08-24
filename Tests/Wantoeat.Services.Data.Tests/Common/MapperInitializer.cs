namespace Wantoeat.Services.Data.Tests.Common
{
    using System.Reflection;

    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels;
    using Wantoeat.Web.ViewModels.Allergens;
    using Wantoeat.Web.ViewModels.Ingredients;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(IngredientEditInputModel).GetTypeInfo().Assembly,
                typeof(IngredientSimpleViewModel).GetTypeInfo().Assembly,
                typeof(AllergenDetailViewModel).GetTypeInfo().Assembly);
        }
    }
}
