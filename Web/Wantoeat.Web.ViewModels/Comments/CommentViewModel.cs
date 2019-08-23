namespace Wantoeat.Web.ViewModels.Comments
{
    using AutoMapper;
    using System.Linq;
    using Wantoeat.Data.Models;
    using Wantoeat.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsPrivate { get; set; }

        public string ApplicationUserUserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Comment, CommentViewModel>()
                .ForMember(destination => destination.ApplicationUserUserName,
                          opts => opts.MapFrom(origin => origin.User.UserName));
        }
    }
}
