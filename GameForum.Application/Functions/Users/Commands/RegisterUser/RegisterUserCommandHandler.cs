using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OneOf<Success, NotValidateResponse, IdentityErrorResponse, Error>>
    {
        private UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OneOf<Success, NotValidateResponse, IdentityErrorResponse, Error>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserCommandValidator();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var user = new ApplicationUser { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new Success();
            }

            if (result.Errors is not null)
            {
                return new IdentityErrorResponse(result.Errors.ToList());
            }

            return new Error();
        }
    }
}
