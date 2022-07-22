using GameForum.Application.Functions.Posts.Commands.CreatePost;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameForum.Api.Controllers
{
    [Route($"api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost(Name = "AddPost")]
        public async Task<IActionResult> Create([FromBody] CreatedPostCommand createdPostCommand)
        {
            createdPostCommand.AuthorId = User.FindFirstValue(ClaimTypes.Sid);

            var result = await _mediator.Send(createdPostCommand);

            return result.Match<IActionResult>(
                success => Created($"api/subject/{success.Value.TopicId}/post/{success.Value.PostId}", success.Value),
                notValidate => BadRequest(notValidate.ValidationErrors));
        }

        [Authorize]
        [HttpPatch(Name = "UpdatePostContent")]
        public async Task<IActionResult> UpdateContent([FromBody] UpdatePostContentCommand updatePostContentCommand)
        {
            var result = await _mediator.Send(updatePostContentCommand);

            return result.Match<IActionResult>(success => NoContent(), notValidate => BadRequest(notValidate.ValidationErrors),
                notFound => BadRequest());
        }
    }
}
