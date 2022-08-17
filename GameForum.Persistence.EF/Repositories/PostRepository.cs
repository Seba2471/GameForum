using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Persistence.EF.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IMapper _mapper;

        public PostRepository(GameForumContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<PaginationResponse<PostDto>> GetPageByTopicIdAsync(int pageNumber, int pageSize, int topicId)
        {
            var postsBaseQuery = _dbContext.Posts.OrderBy(t => t.CreatedDate);

            var postsFromDb = await postsBaseQuery
                .Include(p => p.Author)
                .Where(p => p.TopicId == topicId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var totalCount = postsBaseQuery.Count();

            var posts = _mapper.Map<List<PostDto>>(postsFromDb);

            return new PaginationResponse<PostDto>(posts, totalCount, pageSize, pageNumber);
        }

        public async Task<bool> PostExists(int postId)
        {
            var result = await _dbContext.Posts.AnyAsync(p => p.PostId == postId);

            return result;
        }
    }
}
