using GameForum.Application.Functions.Users.Commands.LoginUser;
using GameForum.Application.Functions.Users.Commands.RefreshToken;
using GameForum.Application.Functions.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register(RegisterUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Match<IActionResult>(success => Ok("User created"), notValidate => BadRequest(notValidate.ValidationErrors),
               identityErrors => BadRequest(identityErrors.IdentityErrors), error => BadRequest("Something went wrong"));
        }


        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] SignInUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Match<IActionResult>(success => Ok(success.Value), identityError => BadRequest(identityError.IdentityErrors),
                notValidate => BadRequest(notValidate.ValidationErrors));
        }

        [HttpPost("refresh", Name = "refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Match<IActionResult>(success => Ok(success.Value), identityError => BadRequest(identityError.IdentityErrors),
                notValidate => BadRequest(notValidate.ValidationErrors));
        }
    }
}
