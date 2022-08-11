using GameForum.Application.Functions.Pagination;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQuery : PaginationRequest, IRequest<PaginationResponse<Topic>>
    { }
}
