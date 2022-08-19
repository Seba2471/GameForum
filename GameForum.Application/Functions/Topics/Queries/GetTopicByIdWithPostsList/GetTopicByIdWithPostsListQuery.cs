using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{

    using HandlerResponse = OneOf<Success<TopicDetailWithPostsDto>, NotValidateResponse>;
    public class GetTopicByIdWithPostsListQuery : PaginationQuery, IRequest<HandlerResponse>
    {
        public int Id { get; set; }
    }
}
