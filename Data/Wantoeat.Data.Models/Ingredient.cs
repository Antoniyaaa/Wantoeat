namespace Wantoeat.Data.Models
{
    using System.Collections.Generic;

    using Wantoeat.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.RecipeIngredients = new HashSet<RecipeIngredient>();
            this.IngredientAllergens = new HashSet<IngredientAllergen>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<IngredientAllergen> IngredientAllergens { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public string ImagePath { get; set; }
    }
}
