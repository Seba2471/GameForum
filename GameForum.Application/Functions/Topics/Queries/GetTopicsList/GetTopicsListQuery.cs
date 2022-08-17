using GameForum.Application.Models.Pagination;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQuery : PaginationQuery, IRequest<PaginationResponse<TopicDto>>
    {

    }
}
