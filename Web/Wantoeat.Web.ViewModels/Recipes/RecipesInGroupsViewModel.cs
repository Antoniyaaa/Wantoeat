namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    public class RecipesInGroupsViewModel<K, T>
    {
        public string GroupName { get; set; }

        public List<RecipeSimpleWithCategoryViewModel> Recipes { get; set; }
    }
}
