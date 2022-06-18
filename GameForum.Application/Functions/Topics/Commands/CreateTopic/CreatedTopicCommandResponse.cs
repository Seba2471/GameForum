using FluentValidation.Results;
using GameForum.Application.Responses;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommandResponse : BaseResponse
    {
        public int? TopicId { get; set; }
        public CreatedTopicCommandResponse() : base()
        { }
        public CreatedTopicCommandResponse(ValidationResult validationResult) : base(validationResult)
        { }

        public CreatedTopicCommandResponse(string message) : base(message)
        { }

        public CreatedTopicCommandResponse(bool success, string message) : base(success, message)
        { }

        public CreatedTopicCommandResponse(int topicId)
        {
            TopicId = topicId;
        }
    }
}
