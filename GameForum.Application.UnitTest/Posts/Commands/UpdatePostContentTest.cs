using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using GameForum.Application.Functions.Posts.Commands.UpdatePostContent;
using GameForum.Application.Mapper;
using GameForum.Application.Responses;
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
        public async Task Handler_NullContent_NotBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 1;

            string authorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e";

            var postBeforeUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = null,
                AuthorId = authorId
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var postAfterUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            response.Match(null, notValid => notValid, null).ShouldBeOfType<NotValidateResponse>();
            postAfterUpdate.ShouldBeSameAs(postBeforeUpdate);
        }

        [Fact]
        public async Task Handler_EmptyContent_NotBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 1;

            string authorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e";

            var postBeforeUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = "",
                AuthorId = authorId
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var postAfterUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            response.Match(null, notValid => notValid, null).ShouldBeOfType<NotValidateResponse>();
            postAfterUpdate.ShouldBeSameAs(postBeforeUpdate);
        }




        [Fact]
        public async Task Handler_NotPostAuthor_NotBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 1;

            string authorId = "5c59f198";

            var postBeforeUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            string updateContent = new string('*', 30);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = updateContent,
                AuthorId = authorId
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var postAfterUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            response.Match(null, null, notAuthor => notAuthor).ShouldBeOfType<NotAuthorResponse>();
            postAfterUpdate.ShouldBeSameAs(postBeforeUpdate);
        }



        [Fact]
        public async Task Handler_NotExistsPostId_PostNotBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 999999;

            string authorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e";

            string updateContent = new string('*', 30);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = updateContent,
                AuthorId = authorId
            };

            var response = await handler.Handle(command, CancellationToken.None);

            response.Match(null, notValid => notValid, null).ShouldBeOfType<NotValidateResponse>();
        }


        [Fact]
        public async Task Handler_ValidContent_PostBeUpdated()
        {
            var handler = new UpdatePostContentCommandHandler(_mockPostRepository.Object, _mapper);

            int postId = 1;

            string authorId = "5c59f198-a9aa-4a8e-af28-a93b1e62e37e";

            string updateContent = new string('*', 30);

            var command = new UpdatePostContentCommand()
            {
                PostId = postId,
                Content = updateContent,
                AuthorId = authorId
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var postAfterUpdate = await _mockPostRepository.Object.GetByIdAsync(postId);

            response.Match(success => success, null, null).ShouldBeOfType<Success>();
            postAfterUpdate.Content.ShouldBe(updateContent);
        }
    }
}
