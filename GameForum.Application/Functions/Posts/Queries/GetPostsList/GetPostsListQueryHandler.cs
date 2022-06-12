using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Posts.Queries.GetPostList
{
    public class GetPostsListQueryHandler : IRequestHandler<GetPostsListQuery, List<PostInListViewModel>>
    {
        private readonly IAsyncRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public GetPostsListQueryHandler(IMapper mapper, IAsyncRepository<Post> postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<List<PostInListViewModel>> Handle(GetPostsListQuery request, CancellationToken cancellationToken)
        {
            var all = await _postRepository.GetAllAsync();
            var allordered = all.OrderBy(x => x.Created);

            return _mapper.Map<List<PostInListViewModel>>(allordered);
        }
    }
}
