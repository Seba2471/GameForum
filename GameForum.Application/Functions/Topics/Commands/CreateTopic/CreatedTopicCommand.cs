using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Text.Json.Serialization;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    using HandlerResponse = OneOf<Success<CreatedTopicCommandResponse>, NotValidateResponse>;
    public class CreatedTopicCommand : IRequest<HandlerResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public string AuthorId { get; set; }
    }
}
