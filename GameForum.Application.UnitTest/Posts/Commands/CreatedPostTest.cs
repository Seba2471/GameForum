using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Posts.Commands.CreatePost;
using GameForum.Application.Mapper;
using GameForum.Application.Responses;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using OneOf.Types;
using Shouldly;

namespace GameForum.Application.UnitTest.Posts.Commands
{


    public class CreatedPostTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<ITopicRepository> _mockTopicRepository;

        public CreatedPostTest()
        {
            _mockPostRepository = RepositoryMocks.GetPostRepository();
            _mockTopicRepository = RepositoryMocks.GetTopicRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handler_NotValidPost_TopicNotExists()
        {
            var handler = new CreatedPostCommandHandler(_mockPostRepository.Object, _mapper, _mockTopicRepository.Object);

            var allPostsBeforeCount = (await _mockPostRepository.Object.GetAllAsync()).Count();

            var command = new CreatedPostCommand()
            {
                Content = new string('*', 30),

                TopicId = 99999999,

                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockPostRepository.Object.GetAllAsync();

            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
        }

        [Fact]
        public async Task Handler_ValidPost_AddedToPostRepo()
        {
            var handler = new CreatedPostCommandHandler(_mockPostRepository.Object, _mapper, _mockTopicRepository.Object);

            var allPostsBeforeCount = (await _mockPostRepository.Object.GetAllAsync()).Count();

            var command = new CreatedPostCommand()
            {
                Content = new string('*', 30),

                TopicId = 1,

                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockPostRepository.Object.GetAllAsync();

            response.Match(success => success, null).ShouldBeOfType<Success<CreatedPostCommandResponse>>();
            allPosts.Count.ShouldBe(allPostsBeforeCount + 1);
        }

        [Fact]
        public async Task Handler_NotValidPost_TooLongContent_501Characters_NotAddedToPostRepo()
        {
            var handler = new CreatedPostCommandHandler(_mockPostRepository.Object, _mapper, _mockTopicRepository.Object);

            var allPostsBeforeCount = (await _mockPostRepository.Object.GetAllAsync()).Count();

            var command = new CreatedPostCommand()
            {
                Content = new string('*', 501),

                TopicId = 1,

                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockPostRepository.Object.GetAllAsync();

            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
        }

        [Fact]
        public async Task Handler_NotVavlidPost_TooShortContent_2Characters_NotAddedToPostRepo()
        {
            var handler = new CreatedPostCommandHandler(_mockPostRepository.Object, _mapper, _mockTopicRepository.Object);

            var allPostsBeforeCount = (await _mockPostRepository.Object.GetAllAsync()).Count();

            var command = new CreatedPostCommand()
            {
                Content = new string('*', 2),

                TopicId = 1,

                AuthorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e"
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allPosts = await _mockPostRepository.Object.GetAllAsync();

            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allPosts.Count.ShouldBe(allPostsBeforeCount);
        }
    }
}
