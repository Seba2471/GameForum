using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Mapper;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using Shouldly;

namespace GameForum.Application.UnitTest.Topics.Commands
{
    public class CreatedTopicTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITopicRepository> _mockTopicRepository;

        public CreatedTopicTest()
        {
            _mockTopicRepository = RepositoryMocks.GetTopicRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidTopic_AddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allPostsBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 10),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockTopicRepository.Object.GetAllAsync();

            response.Success.ShouldBe(true);
            response.ValidationErrors.Count.ShouldBe(0);
            allPosts.Count.ShouldBe(allPostsBeforeCount + 1);
            response.TopicId.ShouldNotBeNull();
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooShortTitle_2Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allPostsBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 2),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockTopicRepository.Object.GetAllAsync();

            response.Success.ShouldBe(false);
            response.ValidationErrors.Count.ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
            response.TopicId.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooLongTitle_31Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allPostsBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 81),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockTopicRepository.Object.GetAllAsync();

            response.Success.ShouldBe(false);
            response.ValidationErrors.Count.ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
            response.TopicId.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooShortContent_2Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allPostsBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 25),
                Content = new string('*', 2),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockTopicRepository.Object.GetAllAsync();

            response.Success.ShouldBe(false);
            response.ValidationErrors.Count.ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
            response.TopicId.ShouldBeNull();
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooLongContent_501Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allPostsBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 25),
                Content = new string('*', 501),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockTopicRepository.Object.GetAllAsync();

            response.Success.ShouldBe(false);
            response.ValidationErrors.Count.ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
            response.TopicId.ShouldBeNull();
        }

    }
}
