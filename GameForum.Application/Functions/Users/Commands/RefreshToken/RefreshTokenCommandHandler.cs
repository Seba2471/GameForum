using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Functions.Users.Commands.SignInUser;
using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, OneOf<Success<SignInUserCommandResponse>,
        IdentityErrorResponse, NotValidateResponse>>
    {
        private UserManager<ApplicationUser> _userManager;
        private ITokenRepository<ApplicationUser> _tokenRepository;
        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, ITokenRepository<ApplicationUser> tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        public async Task<OneOf<Success<SignInUserCommandResponse>, IdentityErrorResponse, NotValidateResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var validator = new RefreshTokenCommandValidator();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var isValid = _tokenRepository.ValidateRefreshToken(request.RefreshToken);

            if (!isValid)
            {
                return new IdentityErrorResponse("RefreshToken", "Refresh token not valid");
            }

            var refreshTokenFromDb = await _tokenRepository.GetByTokenValue(request.RefreshToken);

            await _tokenRepository.DeleteAsync(refreshTokenFromDb);

            var user = await _userManager.FindByIdAsync(refreshTokenFromDb.UserId);

            var roles = await _userManager.GetRolesAsync(user);


            var accessToken = _tokenRepository.GenerateAccessToken(user, roles);

            var refreshToken = _tokenRepository.GenereateRefreshToken(user);

            await _tokenRepository.AddAsync(refreshToken);

            var response = new SignInUserCommandResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.RefreshTokenValue
            };


            return new Success<SignInUserCommandResponse>(response);

        }
    }
}
