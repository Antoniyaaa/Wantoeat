using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wantoeat.Data.Common.Models;

namespace Wantoeat.Data.Models
{
    public class Allergen : BaseDeletableModel<int>
    {
        public Allergen()
        {
            this.RecipeAllergens = new HashSet<RecipeAllergen>();
            this.IngredientAllergens = new HashSet<IngredientAllergen>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<RecipeAllergen> RecipeAllergens { get; set; }

        public virtual ICollection<IngredientAllergen> IngredientAllergens { get; set; }

    }
}
