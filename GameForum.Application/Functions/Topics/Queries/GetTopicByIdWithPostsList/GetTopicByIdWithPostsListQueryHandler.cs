using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{
    using HandlerResponse = OneOf<Success<TopicDetailWithPostsDto>, NotValidateResponse>;
    public class GetTopicByIdWithPostsListQueryHandler : IRequestHandler<GetTopicByIdWithPostsListQuery, HandlerResponse>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetTopicByIdWithPostsListQueryHandler(ITopicRepository topicRepository, IPostRepository postRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<HandlerResponse> Handle(GetTopicByIdWithPostsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTopicByIdWithPostsListQueryValidation(_topicRepository);

            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var topic = await _topicRepository.GetByIdWithAuthor(request.Id);

            var authorDto = _mapper.Map<AuthorDto>(topic.Author);

            var posts = await _postRepository.GetPageByTopicIdAsync(request.PageNumber, request.PageSize, request.Id);

            if (posts.Items.Count == 0)
            {
                return new NotValidateResponse("PageNumber", "Page not exists");
            }

            var response = new TopicDetailWithPostsDto()
            {
                TopicId = topic.TopicId,
                Author = authorDto,
                Content = topic.Content,
                Title = topic.Title,
                Posts = posts
            };

            return new Success<TopicDetailWithPostsDto>(response);
        }
    }
}
