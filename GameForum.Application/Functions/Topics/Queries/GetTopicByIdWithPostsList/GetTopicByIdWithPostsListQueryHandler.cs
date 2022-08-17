using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{
    public class GetTopicByIdWithPostsListQueryHandler : IRequestHandler<GetTopicByIdWithPostsListQuery, TopicDetailWithPostsDto>
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

        public async Task<TopicDetailWithPostsDto> Handle(GetTopicByIdWithPostsListQuery request, CancellationToken cancellationToken)
        {

            var topic = await _topicRepository.GetByIdWithAuthor(request.Id);

            var authorDto = _mapper.Map<AuthorDto>(topic.Author);

            var posts = await _postRepository.GetPageByTopicIdAsync(request.PageNumber, request.PageSize, request.Id);

            return new TopicDetailWithPostsDto()
            {
                TopicId = topic.TopicId,
                Author = authorDto,
                Content = topic.Content,
                Title = topic.Title,
                Posts = posts
            };
        }
    }
}
