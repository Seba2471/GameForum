using GameForum.Application.Responses;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<OneOf<Success, NotValidateResponse, IdentityErrorResponse, Error>>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
