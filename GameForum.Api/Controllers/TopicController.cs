using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddTopic")]
        public async Task<ActionResult<int>> Create([FromBody] CreatedTopicCommand createdTopicCommand)
        {
            var result = await _mediator.Send(createdTopicCommand);

            return Ok(result);
        }
    }
}
