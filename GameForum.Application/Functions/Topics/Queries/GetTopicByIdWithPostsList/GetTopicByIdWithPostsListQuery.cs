using GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery
{
    public class GetTopicByIdWithPostsListQuery : IRequest<List<TopicWithByIdPostsListViewModel>>
    {
        public int Id { get; set; }
    }
}
