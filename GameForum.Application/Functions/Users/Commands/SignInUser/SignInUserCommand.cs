using GameForum.Application.Functions.Users.Commands.SignInUser;
using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.LoginUser
{
    public class SignInUserCommand : IRequest<OneOf<Success<SignInUserCommandResponse>, IdentityErrorResponse, NotValidateResponse>>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
