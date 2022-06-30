using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using GameForum.Application.Functions.Posts.Commands.UpdatePostContent;
using GameForum.Application.Mapper;
using GameForum.Application.UnitTest.Mocks;
using Moq;
using OneOf.Types;
using Shouldly;

namespace GameForum.Application.UnitTest.Posts.Commands
{
    public class UpdatePostContentTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _mockPostRepository;

        public UpdatePostContentTest()
        {
            _mockPostRepository = RepositoryMocks.GetPostRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handler_ValidContent_PostBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 1;

            string updateContent = new string('*', 30);

            //var postBeforeUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = updateContent
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var postAfterUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            response.Match(success => success, null, null).ShouldBeOfType<Success>();
            postAfterUpdate.Content.ShouldBe(updateContent);
        }
    }
}
