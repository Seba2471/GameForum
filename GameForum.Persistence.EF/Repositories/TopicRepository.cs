using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Persistence.EF.Repositories
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(GameForumContext dbContext) : base(dbContext)
        { }

        public async Task<Topic?> GetTopicByIdWithPostsList(int topicId)
        {
            return await _dbContext.Topics.Include(t => t.Posts).FirstOrDefaultAsync(t => t.TopicId == topicId);
        }
        public async Task<bool> TopicExists(int topicId)
        {
            return await _dbContext.Topics.AnyAsync(t => t.TopicId == topicId);
        }
    }
}
