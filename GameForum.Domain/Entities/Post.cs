namespace GameForum.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
