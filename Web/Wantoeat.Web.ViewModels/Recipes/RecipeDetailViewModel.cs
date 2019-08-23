namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Allergens;
    using Wantoeat.Web.ViewModels.Comments;
    using Wantoeat.Web.ViewModels.Ingredients;

    public class RecipeDetailViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Cooking time")]
        public string CookingTimeName { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<IngredientForRecipeViewModel> RecipeIngredient { get; set; }

        public ICollection<AllergenSimpleViewModel> RecipeAllergens { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public string ImagePath { get; set; }
    }
}
