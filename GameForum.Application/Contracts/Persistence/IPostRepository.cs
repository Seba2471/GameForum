using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<bool> PostExists(int postId);
    }
}
