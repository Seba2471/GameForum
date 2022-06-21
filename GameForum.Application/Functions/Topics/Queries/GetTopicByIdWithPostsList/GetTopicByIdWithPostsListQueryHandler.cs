using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList
{
    public class GetTopicByIdWithPostsListQueryHandler : IRequestHandler<GetTopicByIdWithPostsListQuery, Topic>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public GetTopicByIdWithPostsListQueryHandler(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<Topic> Handle(GetTopicByIdWithPostsListQuery request, CancellationToken cancellationToken)
        {
            return await _topicRepository.GetTopicByIdWithPostsList(request.Id);
        }
    }
}
