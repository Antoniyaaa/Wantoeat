namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipeAllViewModel
    {
        public IEnumerable<RecipeSimpleViewModel> Recipes { get; set; }
    }
}
