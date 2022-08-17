namespace GameForum.Application.Models
{
    public class PostDto
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int TopicId { get; set; }
        public AuthorDto Author { get; set; }
    }
}
