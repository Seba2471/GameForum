using AutoMapper;
using GameForum.Application.Contracts.Persistence;
using GameForum.Domain.Entities;
using MediatR;

namespace GameForum.Application.Functions.Topics.Commands.CreateTopic
{
    public class CreatedTopicCommandHandler : IRequestHandler<CreatedTopicCommand, CreatedTopicCommandResponse>
    {

        private readonly ITopicRepository _topicRespository;
        private readonly IMapper _mapper;

        public CreatedTopicCommandHandler(ITopicRepository topicRespository, IMapper mapper)
        {
            _topicRespository = topicRespository;
            _mapper = mapper;
        }

        public async Task<CreatedTopicCommandResponse> Handle(CreatedTopicCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatedTopicCommandValidator();

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreatedTopicCommandResponse(validatorResult);
            }

            var topic = _mapper.Map<Topic>(request);

            await _topicRespository.AddAsync(topic);

            return new CreatedTopicCommandResponse(topic.TopicId);
        }
    }
}
