using FluentValidation;

namespace GameForum.Application.Functions.Topics.Queries.GetTopicsList
{
    public class GetTopicListQueryValidation : AbstractValidator<GetTopicsListQuery>
    {
        public GetTopicListQueryValidation()
        {
            RuleFor(q => q.PageSize)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();

            RuleFor(q => q.PageNumber)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();
        }
    }
}
