using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;


namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    using HandlerResponse = OneOf<Success<CreatedPostCommandResponse>, NotValidateResponse>;
    public class CreatedPostCommand : IRequest<HandlerResponse>
    {
        public string Content { get; set; }
        public int TopicId { get; set; }
    }
}
