using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList
{
    public class GetTopicWithPostsListQueryHandler : IRequestHandler<GetTopicWithPostsListQuery, List<TopicWithPostsListViewModel>>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicWithPostsListQueryHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<List<TopicWithPostsListViewModel>> Handle(GetTopicWithPostsListQuery request, CancellationToken cancellationToken)
        {
            var topic = await _topicRepository.GetTopicWithPostsList();

            return _mapper.Map<List<TopicWithPostsListViewModel>>(topic);
        }
    }
}
