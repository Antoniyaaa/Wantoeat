namespace Wantoeat.Web.ViewModels.Ingredients
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class RecipeCreateIngredientViewModel : IMapFrom<Ingredient>
    {
        public string Name { get; set; }

    }
}
