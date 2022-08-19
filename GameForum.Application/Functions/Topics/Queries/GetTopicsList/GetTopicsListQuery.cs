using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    using HandlerResponse = OneOf<Success<PaginationResponse<TopicDto>>, NotValidateResponse>;
    public class GetTopicsListQuery : PaginationQuery, IRequest<HandlerResponse>
    {

    }
}
