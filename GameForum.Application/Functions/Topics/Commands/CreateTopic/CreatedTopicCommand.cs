using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    using HandlerResponse = OneOf<Success<CreatedTopicCommandResponse>, NotValidateResponse>;
    public class CreatedTopicCommand : IRequest<HandlerResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
