using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;

namespace GameForum.Persistence.EF.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(GameForumContext dbContext) : base(dbContext)
        { }

    }
}
