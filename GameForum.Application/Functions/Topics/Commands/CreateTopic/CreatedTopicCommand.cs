using MediatR;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommand : IRequest<CreatedTopicCommandResponse>
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
