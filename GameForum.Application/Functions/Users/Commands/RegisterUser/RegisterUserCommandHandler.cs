﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OneOf<Success, Error>>
    {
        private UserManager<IdentityUser> _userManager;

        public RegisterUserCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OneOf<Success, Error>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser { UserName = request.UserName, Email = "test@gmail.com" };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new Success();
            }

            return new Error();
        }
    }
}
