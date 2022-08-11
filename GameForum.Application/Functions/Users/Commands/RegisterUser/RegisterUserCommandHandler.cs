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
        private RoleManager<IdentityRole> _roleManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                var role = _roleManager.FindByNameAsync("User").Result;

                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }


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
