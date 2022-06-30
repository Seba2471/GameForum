using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Posts.Commands.UpdatePost
{
    using HandlerResponse = OneOf<Success, NotValidateResponse, NotFound>;
    public class UpdatePostContentCommand : IRequest<HandlerResponse>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int TopicId { get; set; }
    }
}
