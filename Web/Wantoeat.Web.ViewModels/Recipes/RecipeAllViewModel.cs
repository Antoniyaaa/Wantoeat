namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Wantoeat.Web.ViewModels.Allergens;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class RecipeAllViewModel
    {
        public RecipeAllViewModel()
        {
            Ingredients = new List<RecipeCreateIngredientViewModel>();
            Allergens = new List<AllergenSimpleViewModel>();
        }

        public IEnumerable<RecipeSimpleViewModel> Recipes { get; set; }

        public IEnumerable<RecipeCreateIngredientViewModel> Ingredients { get; set; }

        public IEnumerable<AllergenSimpleViewModel> Allergens { get; set; }

    }
}
