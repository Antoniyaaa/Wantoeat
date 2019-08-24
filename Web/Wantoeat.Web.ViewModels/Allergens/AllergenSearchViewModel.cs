namespace Wantoeat.Web.ViewModels.Allergens
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class AllergenSearchViewModel : IMapFrom<Allergen>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public bool Selected { get; set; }
    }
}
