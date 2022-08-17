using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<bool> PostExists(int postId);
        Task<PaginationResponse<PostDto>> GetPageByTopicIdAsync(int pageNumber, int pageSize, int topicId);
    }
}
