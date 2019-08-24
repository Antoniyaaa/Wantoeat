namespace Wantoeat.Web.ViewModels.Allergens
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class IngredientCreateAllergenViewModel : IMapFrom<Allergen>
    {
        public string Name { get; set; }

    }
}
