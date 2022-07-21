using FluentValidation;

namespace GameForum.Application.Functions.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(r => r.RefreshToken)
                .NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
