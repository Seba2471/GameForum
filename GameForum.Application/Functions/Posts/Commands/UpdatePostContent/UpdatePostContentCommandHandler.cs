using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Posts.Commands.UpdatePostContent
{
    using HandlerResponse = OneOf<Success<UpdatePostContentCommandResponse>, NotValidateResponse, NotAuthorResponse>;
    public class UpdatePostContentCommandHandler : IRequestHandler<UpdatePostContentCommand, HandlerResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public UpdatePostContentCommandHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }


        public async Task<HandlerResponse> Handle(UpdatePostContentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePostContentCommandValidator(_postRepository);

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }


            var post = await _postRepository.GetByIdAsync(request.PostId);

            if (post.AuthorId != request.AuthorId)
            {
                return new NotAuthorResponse("post");
            }

            post.Content = request.Content;

            await _postRepository.UpdateAsync(post);

            var postResponse = _mapper.Map<UpdatePostContentCommandResponse>(post);

            return new Success<UpdatePostContentCommandResponse>(postResponse);
        }
    }
}
