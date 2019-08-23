namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Wantoeat.Web.ViewModels.Ingredients;

    public class RecipeAllViewModel
    {
        public IEnumerable<RecipeSimpleViewModel> Recipes { get; set; }

        public IEnumerable<RecipeCreateIngredientViewModel> Ingredients { get; set; }
    }
}
