namespace Wantoeat.Web.ViewModels.Ingredients
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class IngredientForRecipeViewModel : IMapFrom<RecipeIngredient>
    {
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
