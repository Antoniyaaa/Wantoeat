using System;
using System.Collections.Generic;
using System.Text;

namespace Wantoeat.Data.Models
{
    public class RecipeAllergen
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }
    }
}
