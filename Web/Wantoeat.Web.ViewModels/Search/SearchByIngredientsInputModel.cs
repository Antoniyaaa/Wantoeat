using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wantoeat.Data.Models;
using Wantoeat.Services.Mapping;
using Wantoeat.Web.ViewModels.Ingredients;

namespace Wantoeat.Web.ViewModels.Search
{
    public class SearchByIngredientsInputModel
    {
        public IngredientSearchViewModel[] Ingredients { get; set; }
    }

    public class IngredientSearchViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }

        public bool Selected { get; set; }

    }
}
