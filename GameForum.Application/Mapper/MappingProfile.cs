using AutoMapper;
using GameForum.Application.Functions.Posts.Commands.CreatePost;
using GameForum.Application.Functions.Posts.Commands.UpdatePost;
using GameForum.Application.Functions.Posts.Commands.UpdatePostContent;
using GameForum.Application.Functions.Topics.Commands.CreateTopic;
using GameForum.Application.Models;
using GameForum.Domain.Entities;

namespace GameForum.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Topic, TopicDto>();

            CreateMap<Post, CreatedPostCommand>()
                .ReverseMap();

            CreateMap<Topic, CreatedTopicCommand>()
                .ReverseMap();

            CreateMap<Topic, TopicDetailWithPostsDto>()
                .ReverseMap();

            CreateMap<CreatedTopicCommandResponse, Topic>()
                .ReverseMap();

            CreateMap<CreatedPostCommandResponse, Post>()
                .ReverseMap();

            CreateMap<Post, UpdatePostContentCommand>()
                .ReverseMap();

            CreateMap<Post, UpdatePostContentCommandResponse>()
                .ReverseMap();

            CreateMap<Post, PostDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, AuthorDto>();
        }
    }
}
