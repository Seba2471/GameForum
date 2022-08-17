using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITopicRepository : IAsyncRepository<Topic>
    {
        Task<bool> TopicExists(int topicId);
        Task<PaginationResponse<TopicDto>> GetPageAsync(int pageNumber, int pageSize);
        Task<TopicDetailWithPostsDto> GetTopicByIdWithPosts(int topicId, int pageNumber, int pageSize);
    }
}
