namespace Wantoeat.Web.ViewModels.Comments
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;
    using Wantoeat.Web.ViewModels.Ingredients;
    using Wantoeat.Web.ViewModels.Recipes;

    public class CommentInputModel : IMapTo<Comment>
    {
        public int RecipeId { get; set; }

        public CommentCreateRecipeViewModel Recipe { get; set; }

        [Required]
        [StringLength(200)]
        public string Content { get; set; }

        public bool IsPrivate { get; set; }

        public string UserId { get; set; }
    }
}
