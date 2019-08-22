using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Wantoeat.Data.Common.Models;

namespace Wantoeat.Data.Models
{
    public class CookingTime : BaseDeletableModel<int>
    {
        public CookingTime()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
