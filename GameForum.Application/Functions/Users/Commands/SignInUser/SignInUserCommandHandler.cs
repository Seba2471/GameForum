using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Users.Commands.LoginUser;
using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.SignInUser
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, OneOf<Success<SignInUserCommandResponse>, IdentityErrorResponse, NotValidateResponse>>
    {
        private UserManager<ApplicationUser> _userManager;
        private ITokenRepository<ApplicationUser> _tokenRepository;
        public SignInUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenRepository<ApplicationUser> tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        public async Task<OneOf<Success<SignInUserCommandResponse>, IdentityErrorResponse, NotValidateResponse>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new SignInUserCommandValidator();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var user = await _userManager.FindByNameAsync(request.userName);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.password))
            {
                return new IdentityErrorResponse("LoginFailed", "Incorrect user name or password");
            }

            var roles = await _userManager.GetRolesAsync(user);


            var accessToken = _tokenRepository.GenerateAccessToken(user, roles);

            var refreshToken = _tokenRepository.GenereateRefreshToken(user);

            await _tokenRepository.AddRefreshTokenAsync(refreshToken);

            var response = new SignInUserCommandResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.RefreshTokenValue
            };


            return new Success<SignInUserCommandResponse>(response);
        }
    }
}
