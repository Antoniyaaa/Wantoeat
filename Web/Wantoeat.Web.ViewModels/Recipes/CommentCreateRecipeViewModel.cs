using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Wantoeat.Data.Models;
using Wantoeat.Services.Mapping;
using Wantoeat.Web.ViewModels.Ingredients;

namespace Wantoeat.Web.ViewModels.Recipes
{
    public class CommentCreateRecipeViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int RecipeId { get; set; }

        public string RecipeName { get; set; }

        public string RecipeDescription { get; set; }

        public ICollection<IngredientForRecipeViewModel> RecipeIngredient { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Recipe, CommentCreateRecipeViewModel>()
                .ForMember(x => x.RecipeId, opts => opts.MapFrom(y => y.Id))
                .ForMember(x => x.RecipeName, opts => opts.MapFrom(y => y.Name))
                .ForMember(x => x.RecipeDescription, opts => opts.MapFrom(y => y.Description));
        }
    }
}
