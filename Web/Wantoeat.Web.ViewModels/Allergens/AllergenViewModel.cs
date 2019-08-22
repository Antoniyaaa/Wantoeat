namespace Wantoeat.Web.ViewModels.Allergens
{
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class AllergenViewModel : IMapFrom<Allergen>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
