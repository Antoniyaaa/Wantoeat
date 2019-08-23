namespace Wantoeat.Web.ViewModels.Ingredients
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class IngredientSearchViewModel : IMapFrom<Ingredient>
    {
            public int Id { get; set; }

            public string Name { get; set; }

            public string ImagePath { get; set; }

            public bool Selected { get; set; }
    }
}
