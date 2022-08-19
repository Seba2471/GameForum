using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Functions.Topics.Queries.GetTopicByIdWithPostsList;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            createdTopicCommand.AuthorId = User.FindFirstValue(ClaimTypes.Sid);

            var result = await _mediator.Send(createdTopicCommand);

            return result.Match<IActionResult>(
                success => Created($"api/topic/{success.Value.TopicId}", success.Value),
                notValidation => BadRequest(notValidation.ValidationErrors));
        }

        [HttpGet("{id}", Name = "GetTopicDetailWithPostList")]
        public async Task<IActionResult> GetTopicDetailWithPostsList([FromRoute] int id, [FromQuery] PaginationQuery paginationQuery)
        {
            var query = new GetTopicByIdWithPostsListQuery()
            {
                Id = id,
                PageNumber = paginationQuery.PageNumber,
                PageSize = paginationQuery.PageSize

            };

            var result = await _mediator.Send(query);

            return result.Match<IActionResult>(success => Ok(success.Value),
                notValid => BadRequest(notValid.ValidationErrors));
        }

        [HttpGet(Name = "GetTopicsList")]
        public async Task<IActionResult> GetTopicsList([FromQuery] GetTopicsListQuery getTopicsListQuery)
        {
            var topics = await _mediator.Send(getTopicsListQuery);

            return Ok(topics);
        }
    }
}
