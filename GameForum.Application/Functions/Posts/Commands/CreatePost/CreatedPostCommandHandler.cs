using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    using HandlerResponse = OneOf<Success<CreatedPostCommandResponse>, NotValidateResponse>;
    public class CreatedPostCommandHandler : IRequestHandler<CreatedPostCommand, HandlerResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public CreatedPostCommandHandler(IPostRepository postRepository, IMapper mapper, ITopicRepository topicRepository)
        {
            _postRepository = postRepository;
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<HandlerResponse> Handle(CreatedPostCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatedPostCommandValidator(_topicRepository);

            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var post = _mapper.Map<Post>(request);

            post.Rate = 0;

            post = await _postRepository.AddAsync(post);

            var postReponse = _mapper.Map<CreatedPostCommandResponse>(post);

            return new Success<CreatedPostCommandResponse>(postReponse);
        }
    }
}
