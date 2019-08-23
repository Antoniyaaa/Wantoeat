namespace Wantoeat.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Comments;

    public class RecipeSimpleViewModel : IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public ICollection<CommentViewModel> CommentContent { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<RecipeIngredient, RecipeSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.RecipeId))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Recipe.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Recipe.ImagePath));

            configuration
                .CreateMap<ApplicationUserFavoriteRecipes, RecipeSimpleViewModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(y => y.RecipeId))
                .ForMember(x => x.Name, opts => opts.MapFrom(y => y.Recipe.Name))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(y => y.Recipe.ImagePath))
                .ForMember(x => x.CommentContent, opts => opts.MapFrom(y => y.Recipe.Comments));

        }
    }
}
