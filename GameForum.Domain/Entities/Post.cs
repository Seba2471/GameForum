using GameForum.Domain.Common;

namespace GameForum.Domain.Entities
{
    public class Post : AuditableEntity
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
