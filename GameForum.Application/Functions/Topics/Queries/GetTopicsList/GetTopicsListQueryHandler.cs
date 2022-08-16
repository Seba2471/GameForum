using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Pagination;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQueryHandler : IRequestHandler<GetTopicsListQuery, PaginationResponse<TopicDto>>
    {
        private ITopicRepository _topicRepository;
        public GetTopicsListQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public Task<PaginationResponse<TopicDto>> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            return _topicRepository.GetPageAsync(request.PageNumber, request.PageSize);
        }
    }
}
