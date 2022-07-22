using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Posts.Commands.UpdatePostContent
{
    using HandlerResponse = OneOf<Success, NotValidateResponse, NotFound>;
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
            var validator = new UpdatePostContentCommandValidator();

            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            bool postExists = await _postRepository.IsPostExists(request.PostId);

            if (!postExists)
            {
                return new NotFound();
            }

            var post = _mapper.Map<Post>(request);

            await _postRepository.UpdateAsync(post);

            return new Success();
        }
    }
}
