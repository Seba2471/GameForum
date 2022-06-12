using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQueryHandler : IRequestHandler<GetTopicsListQuery, List<TopicInListViewModel>>
    {
        private readonly IAsyncRepository<Topic> _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicsListQueryHandler(IAsyncRepository<Topic> topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<List<TopicInListViewModel>> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            var all = await _topicRepository.GetAllAsync();

            var allordered = all.OrderBy(x => x.Created);

            return _mapper.Map<List<TopicInListViewModel>>(all);
        }
    }
}
