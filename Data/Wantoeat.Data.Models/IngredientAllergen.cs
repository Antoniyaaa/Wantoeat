using System;
using System.Collections.Generic;
using System.Text;

namespace Wantoeat.Data.Models
{
    public class IngredientAllergen
    {
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }


        public int AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }
    }
}
