using FluentValidation;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommandValidator : AbstractValidator<CreatedPostCommand>
    {

        public CreatedPostCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short")
                .MaximumLength(500)
                .WithMessage("{PropertyName} is too long");
        }
    }
}
