namespace Wantoeat.Data.Models
{
    using Wantoeat.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        public bool IsPrivate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

    }
}
