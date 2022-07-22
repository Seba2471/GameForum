using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Mapper;
using GameForum.Application.Responses;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using OneOf.Types;
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

            var allTopicBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 10),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allTopics = await _mockTopicRepository.Object.GetAllAsync();

            response.Match(success => success, null).ShouldBeOfType<Success<CreatedTopicCommandResponse>>();
            allTopics.Count.ShouldBe(allTopicBeforeCount + 1);
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooShortTitle_2Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allTopicBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 2),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allTopics = await _mockTopicRepository.Object.GetAllAsync();

            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allTopics.Count.ShouldBe(allTopicBeforeCount);
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooLongTitle_31Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allTopicBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 81),
                Content = new string('*', 30),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allTopics = await _mockTopicRepository.Object.GetAllAsync();


            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allTopics.Count.ShouldBe(allTopicBeforeCount);
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooShortContent_2Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allTopicBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 25),
                Content = new string('*', 2),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allTopics = await _mockTopicRepository.Object.GetAllAsync();


            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allTopics.Count.ShouldBe(allTopicBeforeCount);
        }

        [Fact]
        public async Task Handle_NotValidTopic_TooLongContent_501Characters_NoAddedToTopicRepo()
        {
            var handler = new CreatedTopicCommandHandler(_mockTopicRepository.Object, _mapper);

            var allTopicBeforeCount = (await _mockTopicRepository.Object.GetAllAsync()).Count();

            var command = new CreatedTopicCommand()
            {

                Title = new string('*', 25),
                Content = new string('*', 501),
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allTopics = await _mockTopicRepository.Object.GetAllAsync();


            response.Match(null, validateResponse => validateResponse).ShouldBeOfType<NotValidateResponse>();
            response.Match(null, notValidateResponse => notValidateResponse.ValidationErrors.Count()).ShouldBe(1);
            allTopics.Count.ShouldBe(allTopicBeforeCount);
        }

    }
}
