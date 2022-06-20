using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface ITopicRepository : IAsyncRepository<Topic>
    {
        Task<Topic?> GetTopicByIdWithPostsList(int topicId);
    }
}
