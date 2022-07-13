using GameForum.Application.Functions.Users.Commands.LoginUser;
using GameForum.Application.Functions.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IMediator _mediator;
        public AuthController(UserManager<IdentityUser> userManager, IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register(RegisterUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Match<IActionResult>(success => Ok("User created"), error => BadRequest());
        }


        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] SignInUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Match<IActionResult>(success => Ok(success.Value), error => BadRequest("Incorrect user name or password"));
        }
    }
}
