using GameForum.Domain.Entities;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList
{
    public class TopicWithPostsListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ICollection<Post> Posts { get; set; }
        public Department Department { get; set; }
    }
}
