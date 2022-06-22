namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommandResponse
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public int TopicId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
