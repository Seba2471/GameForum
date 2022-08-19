using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITopicRepository : IAsyncRepository<Topic>
    {
        Task<bool> TopicExists(int topicId);
        Task<Topic> GetByIdWithAuthor(int topicId);
        Task<PaginationResponse<TopicDto>> GetPageAsync(int pageNumber, int pageSize);
    }
}
