using FluentValidation;

namespace GameForum.Application.Functions.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("Bad email address")
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(u => u.UserName)
                .MinimumLength(3)
                .WithMessage("{PropertyName} is too short. Min user name length is 3")
                .MaximumLength(24)
                .WithMessage("{PropertyName} is too long. Max user name length is 24")
                .NotNull()
                .WithMessage("{PropertyName} is required");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
