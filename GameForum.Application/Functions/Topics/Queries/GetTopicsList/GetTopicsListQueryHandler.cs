using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Models;
using GameForum.Application.Models.Pagination;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    using HandlerResponse = OneOf<Success<PaginationResponse<TopicDto>>, NotValidateResponse>;
    public class GetTopicsListQueryHandler : IRequestHandler<GetTopicsListQuery, HandlerResponse>
    {
        private readonly ITopicRepository _topicRepository;
        public GetTopicsListQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<HandlerResponse> Handle(GetTopicsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTopicListQueryValidation();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var result = await _topicRepository.GetPageAsync(request.PageNumber, request.PageSize);

            if (result.Items.Count == 0)
            {
                return new NotValidateResponse("PageNumber", "Page not exists");
            }

            return new Success<PaginationResponse<TopicDto>>(result);
        }
    }
}
