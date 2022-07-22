using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Application.Responses;
using GameForum.Domain.Entities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    using HandlerResponse = OneOf<Success<CreatedTopicCommandResponse>, NotValidateResponse>;
    public class CreatedTopicCommandHandler : IRequestHandler<CreatedTopicCommand, HandlerResponse>
    {

        private readonly ITopicRepository _topicRespository;
        private readonly IMapper _mapper;

        public CreatedTopicCommandHandler(ITopicRepository topicRespository, IMapper mapper)
        {
            _topicRespository = topicRespository;
            _mapper = mapper;
        }

        public async Task<HandlerResponse> Handle(CreatedTopicCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatedTopicCommandValidator();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new NotValidateResponse(validatorResult.Errors);
            }

            var topic = _mapper.Map<Topic>(request);

            await _topicRespository.AddAsync(topic);

            var responseTopic = _mapper.Map<CreatedTopicCommandResponse>(topic);

            return new Success<CreatedTopicCommandResponse>(responseTopic);
        }
    }
}
