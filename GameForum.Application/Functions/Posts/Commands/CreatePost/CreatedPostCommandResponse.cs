using FluentValidation.Results;
using GameForum.Application.Responses;

namespace GameForum.Application.Functions.Posts.Commands.CreatePost
{
    public class CreatedPostCommandResponse : BaseResponse
    {

        public int? PostId { get; set; }
        public CreatedPostCommandResponse() : base()
        { }
        public CreatedPostCommandResponse(ValidationResult validationResult) : base(validationResult)
        { }

        public CreatedPostCommandResponse(string message) : base(message)
        { }

        public CreatedPostCommandResponse(bool success, string message) : base(success, message)
        { }

        public CreatedPostCommandResponse(int postId)
        {
            PostId = postId;
        }
    }
}
