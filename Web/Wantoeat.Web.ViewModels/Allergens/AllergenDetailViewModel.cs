namespace Wantoeat.Web.ViewModels.Allergens
{
    using System.Collections.Generic;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class AllergenDetailViewModel : IMapFrom<Allergen>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<IngredientSimpleViewModel> IngredientAllergens { get; set; }
    }
}
