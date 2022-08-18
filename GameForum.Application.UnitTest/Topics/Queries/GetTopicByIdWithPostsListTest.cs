using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList;
using GameForum.Application.Mapper;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace GameForum.Application.UnitTest.Topics.Queries
{
    public class GetTopicByIdWithPostsListTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITopicRepository> _mockTopicRepository;
        private readonly Mock<IPostRepository> _mockPostRepository;

        public GetTopicByIdWithPostsListTest()
        {
            _mockTopicRepository = RepositoryMocks.GetTopicRepository();
            _mockPostRepository = RepositoryMocks.GetPostRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ReturnDataSuccess()
        {
            var handler = new GetTopicByIdWithPostsListQueryHandler(_mockTopicRepository.Object, _mockPostRepository.Object, _mapper);


            var posts = await _mockPostRepository.Object.GetAllAsync();

            var topicPosts = posts.Where(p => p.TopicId == 1).ToList();


            var command = new GetTopicByIdWithPostsListQuery()
            {
                Id = 1,
                PageSize = 1,
                PageNumber = 2
            };

            var response = await handler.Handle(command, CancellationToken.None);

            response.Posts.TotalItemsCount.ShouldBe(topicPosts.Count());
            response.Posts.Items.Count().ShouldBe(command.PageSize);
        }
    }
}
