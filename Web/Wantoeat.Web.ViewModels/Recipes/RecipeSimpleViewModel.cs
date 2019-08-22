namespace Wantoeat.Web.ViewModels.Recipes
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class RecipeSimpleViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
    }
}
