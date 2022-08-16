using GameForum.Application.Functions.Pagination;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITopicRepository : IAsyncRepository<Topic>
    {
        Task<Topic> GetTopicByIdWithPostsList(int topicId);
        Task<bool> TopicExists(int topicId);
        Task<PaginationResponse<TopicDto>> GetPageAsync(int pageNumber, int pageSize);
    }
}
