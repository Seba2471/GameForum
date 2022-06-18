using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList
{
    public class GetTopicByIdWithPostsListQueryHandler : IRequestHandler<GetTopicByIdWithPostsListQuery, List<TopicWithByIdPostsListViewModel>>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicByIdWithPostsListQueryHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<List<TopicWithByIdPostsListViewModel>> Handle(GetTopicByIdWithPostsListQuery request, CancellationToken cancellationToken)
        {
            var topic = await _topicRepository.GetTopicByIdWithPostsList(request.Id);

            return _mapper.Map<List<TopicWithByIdPostsListViewModel>>(topic);
        }
    }
}
