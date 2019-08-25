using Wantoeat.Data.Models;
using Wantoeat.Services.Mapping;

namespace Wantoeat.Web.ViewModels.Recipes
{
    public class RecipeSimpleWithCategoryViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string CategoryName { get; set; }
    }
}
