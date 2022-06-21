using AutoMapper;
using GameForum.Application.Functions.Posts.Commands.CreatePost;
using GameForum.Application.Functions.Posts.Queries.GetPostList;
using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Functions.Topics.Queries.GetTopicsList;
using GameForum.Application.Functions.Topics.Queries.GetTopicWithPostsList;
using GameForum.Domain.Entities;

namespace GameForum.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostInListViewModel>()
                .ReverseMap();

            CreateMap<Topic, TopicInListViewModel>()
                .ReverseMap();

            CreateMap<Post, CreatedPostCommand>()
                .ReverseMap();

            CreateMap<Topic, CreatedTopicCommand>()
                .ReverseMap();

            CreateMap<Topic, TopicWithByIdPostsListViewModel>()
                .ReverseMap();
        }
    }
}
