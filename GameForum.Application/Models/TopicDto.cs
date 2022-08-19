namespace GameForum.Application.Models
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
