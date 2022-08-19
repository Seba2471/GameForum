using FluentValidation;
using GameForum.Application.Contracts.Persistence;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList
{
    public class GetTopicByIdWithPostsListQueryValidation : AbstractValidator<GetTopicByIdWithPostsListQuery>
    {
        public GetTopicByIdWithPostsListQueryValidation(ITopicRepository topicRepository)
        {
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();

            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (topicId, cancellation) => await topicRepository.TopicExists(topicId))
                .WithMessage("{PropertyName} not exists");
        }
    }
}
