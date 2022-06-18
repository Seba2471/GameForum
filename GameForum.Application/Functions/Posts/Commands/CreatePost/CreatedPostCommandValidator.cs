using FluentValidation;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommandValidator : AbstractValidator<CreatedPostCommand>
    {

        public CreatedPostCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .NotNull();
        }
    }
}
