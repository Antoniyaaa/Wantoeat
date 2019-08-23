using System;
using System.Collections.Generic;
using System.Text;

namespace Wantoeat.Data.Models
{
    public class ApplicationUserFavoriteRecipes
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
