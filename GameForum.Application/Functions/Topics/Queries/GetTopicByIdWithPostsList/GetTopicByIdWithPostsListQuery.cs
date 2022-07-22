using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery
{
    public class GetTopicByIdWithPostsListQuery : IRequest<Topic>
    {
        public int Id { get; set; }
    }
}
