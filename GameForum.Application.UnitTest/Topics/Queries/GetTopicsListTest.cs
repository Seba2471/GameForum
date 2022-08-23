using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Mapper;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace GameForum.Application.UnitTest.Topics.Queries
{
    public class GetTopicsListTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<ITopicRepository> _mockTopicRepository;
        private readonly Mock<IPostRepository> _mockPostRepository;

        public GetTopicsListTest()
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
        public async Task Handler_ReturnSuccessData()
        {
            var handler = new GetTopicsListQueryHandler(_mockTopicRepository.Object);


            var command = new GetTopicsListQuery()
            {
                PageSize = 2,
                PageNumber = 1
            };

            var result = await handler.Handle(command, CancellationToken.None);


            result.Match(success => success.Value.Items.Count, null).ShouldBe(command.PageSize);
        }

        [Fact]
        public async Task Handler_NotValidPageNumber_PageNotExists()
        {
            var handler = new GetTopicsListQueryHandler(_mockTopicRepository.Object);


            var command = new GetTopicsListQuery()
            {
                PageSize = 2,
                PageNumber = 500
            };

            var result = await handler.Handle(command, CancellationToken.None);


            result.Match(null, notValid => notValid.ValidationErrors.Count).ShouldBe(1);
        }

        [Fact]
        public async Task Handler_NotValidPageNumber_NegativeNumber()
        {
            var handler = new GetTopicsListQueryHandler(_mockTopicRepository.Object);

            var command = new GetTopicsListQuery()
            {
                PageSize = 2,
                PageNumber = -5
            };

            var result = await handler.Handle(command, CancellationToken.None);


            result.Match(null, notValid => notValid.ValidationErrors.ContainsKey("PageNumber")).ShouldBe(true);
        }
    }
}
