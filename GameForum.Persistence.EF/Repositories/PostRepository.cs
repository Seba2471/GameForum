using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Persistence.EF.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(GameForumContext dbContext) : base(dbContext)
        { }

        public async Task<bool> PostExists(int postId)
        {
            var result = await _dbContext.Posts.AnyAsync(p => p.PostId == postId);

            return result;
        }
    }
}
