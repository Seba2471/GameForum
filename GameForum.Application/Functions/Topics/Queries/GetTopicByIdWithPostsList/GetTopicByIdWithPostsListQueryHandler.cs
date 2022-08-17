using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{
    public class GetTopicByIdWithPostsListQueryHandler : IRequestHandler<GetTopicByIdWithPostsListQuery, TopicDetailWithPostsDto>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicByIdWithPostsListQueryHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<TopicDetailWithPostsDto> Handle(GetTopicByIdWithPostsListQuery request, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetTopicByIdWithPosts(request.Id, request.PageNumber, request.PageSize);
        }
    }
}
