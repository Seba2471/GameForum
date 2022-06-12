using MediatR;

namespace GameForum.Application.Functions.Posts
{
    public class GetPostsListQuery : IRequest<List<PostInListViewModel>>
    {

    }
}
