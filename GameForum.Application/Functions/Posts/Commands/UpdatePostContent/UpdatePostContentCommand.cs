using GameForum.Application.Functions.Posts.Commands.UpdatePostContent;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Text.Json.Serialization;

namespace GameForum.Application.Functions.Posts.Commands.UpdatePost
{
    using HandlerResponse = OneOf<Success<UpdatePostContentCommandResponse>, NotValidateResponse, NotAuthorResponse>;
    public class UpdatePostContentCommand : IRequest<HandlerResponse>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public string AuthorId { get; set; }
    }
}
