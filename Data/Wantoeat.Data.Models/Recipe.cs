using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wantoeat.Data.Common.Models;

namespace Wantoeat.Data.Models
{
    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.RecipeIngredient = new HashSet<RecipeIngredient>();
            this.RecipeAllergens = new HashSet<RecipeAllergen>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CookingTimeId { get; set; }
        public virtual CookingTime CookingTime { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }

        public virtual ICollection<RecipeAllergen> RecipeAllergens { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ApplicationUserFavoriteRecipes> FavouriteRecipes { get; set; }

        public string ImagePath { get; set; }
    }
}
