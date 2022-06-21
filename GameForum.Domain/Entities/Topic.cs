using GameForum.Domain.Common;

namespace GameForum.Domain.Entities
{
    public class Topic : AuditableEntity
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Post> Posts { get; set; }
    }
}
