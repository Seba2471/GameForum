using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Persistence.EF.Repositories
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        IMapper _mapper;

        public TopicRepository(GameForumContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        public async Task<bool> TopicExists(int topicId)
        {
            return await _dbContext.Topics.AnyAsync(t => t.TopicId == topicId);
        }

        public async Task<PaginationResponse<TopicDto>> GetPageAsync(int pageNumber, int pageSize)
        {
            var baseQuery = _dbContext.Topics.OrderBy(t => t.CreatedDate);


            var itemsFromDb = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var items = _mapper.Map<List<TopicDto>>(itemsFromDb);

            var totalCount = await baseQuery.CountAsync();

            return new PaginationResponse<TopicDto>(items, totalCount, pageSize, pageNumber);
        }

        public async Task<TopicDetailWithPostsDto> GetTopicByIdWithPosts(int topicId, int pageNumber, int pageSize)
        {
            var topic = await _dbContext.Topics
                .Include(t => t.Author)
                .FirstOrDefaultAsync(t => t.TopicId == topicId);

            var postsBaseQuery = _dbContext.Posts.OrderBy(t => t.CreatedDate);

            var postsFromDb = await postsBaseQuery
                .Include(p => p.Author)
                .Where(p => p.TopicId == topicId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await postsBaseQuery.CountAsync();

            if (topic == null)
            {
                return null;
            }

            var author = _mapper.Map<AuthorDto>(topic.Author);

            var posts = _mapper.Map<List<PostDto>>(postsFromDb);

            var paginationResult = new PaginationResponse<PostDto>(posts, totalCount, pageSize, pageNumber);

            return new TopicDetailWithPostsDto()
            {
                TopicId = topic.TopicId,
                Title = topic.Title,
                Content = topic.Content,
                Author = author,
                Posts = paginationResult
            };
        }
    }
}
