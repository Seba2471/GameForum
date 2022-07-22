using FluentValidation;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;

namespace GameForum.Application.Functions.Posts.Commands.UpdatePostContent
{
    public class UpdatePostContentCommandValidator : AbstractValidator<UpdatePostContentCommand>
    {
        public UpdatePostContentCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short")
                .MaximumLength(500)
                .WithMessage("{PropertyName} is too long");

            RuleFor(p => p.PostId)
                .NotNull();

            RuleFor(p => p.TopicId)
                .NotNull();
        }
    }
}
