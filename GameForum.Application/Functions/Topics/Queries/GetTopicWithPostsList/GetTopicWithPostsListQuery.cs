using GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery
{
    public class GetTopicWithPostsListQuery : IRequest<List<TopicWithPostsListViewModel>>
    {

    }
}
