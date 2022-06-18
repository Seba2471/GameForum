namespace GameForum.Application.Functions.Topics.Queries.GetTopicsListWithPosts
{
    public class TopicPostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int TopicId { get; set; }
    }
}
