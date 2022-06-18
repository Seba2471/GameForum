using FluentValidation;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommandValidator : AbstractValidator<CreatedTopicCommand>
    {

        public CreatedTopicCommandValidator()
        {
            RuleFor(t => t.Title)
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short")
                .MaximumLength(30)
                .WithMessage("{PropertyName} is too long")
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(t => t.Content)
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short")
                .MaximumLength(500)
                .WithMessage("{PropertyName} is too long")
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(t => t.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Incorrect department ID");
        }
    }
}
