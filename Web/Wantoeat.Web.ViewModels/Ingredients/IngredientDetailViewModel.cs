namespace Wantoeat.Web.ViewModels.Ingredients
{
    using System.Collections.Generic;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;

    public class IngredientDetailViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AllergenSimpleViewModel> IngredientAllergens { get; set; }

        public string ImagePath { get; set; }
    }
}
