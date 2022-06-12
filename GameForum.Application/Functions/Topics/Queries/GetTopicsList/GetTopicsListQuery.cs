using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicsListQuery : IRequest<List<TopicInListViewModel>>
    {

    }
}
