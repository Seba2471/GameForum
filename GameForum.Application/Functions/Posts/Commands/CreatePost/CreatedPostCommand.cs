using MediatR;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommand : IRequest<CreatedPostCommandResponse>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; } = 0;
        public int TopicId { get; set; }
    }
}
