using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Users.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.SignInUser
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, OneOf<Success<SignInUserCommandResponse>, Error>>
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private ITokenRepository<IdentityUser> _tokenRepository;
        public SignInUserCommandHandler(UserManager<IdentityUser> userManager, IConfiguration config, ITokenRepository<IdentityUser> tokenRepository)
        {
            _userManager = userManager;
            _config = config;
            _tokenRepository = tokenRepository;
        }
        public async Task<OneOf<Success<SignInUserCommandResponse>, Error>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.userName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.password))
            {
                return new Error();
            }

            var roles = await _userManager.GetRolesAsync(user);


            var accessToken = _tokenRepository.GenerateAccessToken(user, roles);

            if (accessToken is null)
            {
                return new Error();
            }

            var response = new SignInUserCommandResponse
            {
                AccessToken = accessToken
            };


            return new Success<SignInUserCommandResponse>(response);
        }
    }
}
