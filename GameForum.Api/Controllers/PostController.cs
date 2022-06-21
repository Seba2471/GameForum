using GameForum.Application.Functions.Posts.Commands.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddPost")]
        public async Task<ActionResult<int>> Create([FromBody] CreatedPostCommand createdPostCommand)
        {
            var result = await _mediator.Send(createdPostCommand);

            return Ok(result);
        }
    }
}
