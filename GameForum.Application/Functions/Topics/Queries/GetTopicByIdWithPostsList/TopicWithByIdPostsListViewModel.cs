using GameForum.Application.Functions.Topics.Queries.GetTopicsListWithPosts;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList
{
    public class TopicWithByIdPostsListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<TopicPostDto> Posts { get; set; }
    }
}
