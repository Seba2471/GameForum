namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class TopicInListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
