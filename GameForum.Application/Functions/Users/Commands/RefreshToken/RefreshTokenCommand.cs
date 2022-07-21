using GameForum.Application.Functions.Users.Commands.SignInUser;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<OneOf<Success<SignInUserCommandResponse>, IdentityErrorResponse, NotValidateResponse>>
    {
        public string RefreshToken { get; set; }
    }
}
