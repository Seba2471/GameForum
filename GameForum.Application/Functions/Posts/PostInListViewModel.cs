using GameForum.Domain.Entities;

namespace GameForum.Application.Functions.Posts
{
    public class PostInListViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rate { get; set; }
        public virtual Topic Topic { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
