using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
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

        [HttpPost("AddTopic")]
        public async Task<ActionResult<int>> Create([FromBody] CreatedTopicCommand createdTopicCommand)
        {
            var result = await _mediator.Send(createdTopicCommand);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetTopicWithPostList")]
        public async Task<ActionResult<int>> GetTopicWithPostsList([FromRoute] int id)
        {
            var query = new GetTopicByIdWithPostsListQuery()
            {
                Id = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
