using MediatR;

namespace GameForum.Application.Functions.Posts.Queries.GetPostList
{
    public class GetPostsListQuery : IRequest<List<PostInListViewModel>>
    {

    }
}
