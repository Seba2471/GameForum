using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<OneOf<Success, Error>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
