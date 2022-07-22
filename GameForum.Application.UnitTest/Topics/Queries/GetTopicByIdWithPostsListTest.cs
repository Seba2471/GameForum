using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
using GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList;
using GameForum.Application.Mapper;
using GameForum.Application.UnitTest.Mocks;
using GameForum.Domain.Entities;
using Moq;
using Shouldly;

namespace GameForum.Application.UnitTest.Topics.Queries
{
    public class GetTopicByIdWithPostsListTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITopicRepository> _mockTopicRepository;


        public GetTopicByIdWithPostsListTest()
        {
            _mockTopicRepository = RepositoryMocks.GetTopicRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetTopicByIdWithPostsTest()
        {
            var handler = new GetTopicByIdWithPostsListQueryHandler(_mockTopicRepository.Object, _mapper);

            var query = new GetTopicByIdWithPostsListQuery() { Id = 1 };

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<Topic>();

            result.Posts.Count.ShouldBe(2);
        }
    }
}
