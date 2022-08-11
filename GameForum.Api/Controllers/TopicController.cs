using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Functions.Topics.Queries.GetTopicsWithPostsListQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost(Name = "AddTopic")]
        public async Task<IActionResult> Create([FromBody] CreatedTopicCommand createdTopicCommand)
        {
            var result = await _mediator.Send(createdTopicCommand);

            return result.Match<IActionResult>(
                success => Created($"api/topic/{success.Value.TopicId}", success.Value),
                notValidation => BadRequest(notValidation.ValidationErrors));
        }

        [Authorize]
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

        [HttpGet(Name = "GetTopicsList")]
        public async Task<IActionResult> GetTopicsList([FromQuery] GetTopicsListQuery getTopicsListQuery)
        {
            var topics = await _mediator.Send(getTopicsListQuery);

            return Ok(topics);
        }
    }
}
