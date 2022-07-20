using FluentValidation;
using GameForum.Application.Functions.Users.Commands.LoginUser;

namespace GameForum.Application.Functions.Users.Commands.SignInUser
{
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserCommandValidator()
        {
            RuleFor(s => s.password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(s => s.userName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}
