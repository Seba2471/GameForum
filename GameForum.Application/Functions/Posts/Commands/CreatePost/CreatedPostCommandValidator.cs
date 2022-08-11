using FluentValidation;
using GameForum.Application.Contracts.Persistence;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommandValidator : AbstractValidator<CreatedPostCommand>
    {


        public CreatedPostCommandValidator(ITopicRepository topicRepository)
        {

            RuleFor(p => p.Content)
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short")
                .MaximumLength(500)
                .WithMessage("{PropertyName} is too long");

            RuleFor(p => p.TopicId)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required")
                .MustAsync(async (topicId, cancellation) => await topicRepository.TopicExists(topicId))
                .WithMessage("{PropertyName} not exists");
        }
    }
}
