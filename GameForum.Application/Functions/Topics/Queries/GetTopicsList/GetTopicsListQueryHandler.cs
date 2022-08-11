using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Pagination;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQueryHandler : IRequestHandler<GetTopicsListQuery, PaginationResponse<Topic>>
    {
        private readonly IAsyncRepository<Topic> _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicsListQueryHandler(IAsyncRepository<Topic> topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<Topic>> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            var all = await _topicRepository.GetPageAsync(request.PageSize, request.PageNumber);

            // var mapped = _mapper.Map<TopicInListViewModel>(all.Items);

            return all;
        }
    }
}
