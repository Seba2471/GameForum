using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{
    public class GetTopicByIdWithPostsListQuery : PaginationQuery, IRequest<TopicDetailWithPostsDto>
    {
        public int Id { get; set; }
    }
}
