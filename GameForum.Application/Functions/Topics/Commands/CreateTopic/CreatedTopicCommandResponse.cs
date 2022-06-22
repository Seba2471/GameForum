namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommandResponse
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
