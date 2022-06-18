using MediatR;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommand : IRequest<CreatedPostCommandResponse>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; } = 0;
        public int TopicId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

    }
}
