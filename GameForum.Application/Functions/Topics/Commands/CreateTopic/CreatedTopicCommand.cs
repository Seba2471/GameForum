using MediatR;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommand : IRequest<CreatedTopicCommandResponse>
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
