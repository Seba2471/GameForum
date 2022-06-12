using AutoMapper;
using GameForum.Application.Functions.Posts.Queries.GetPostList;
using GameForum.Domain.Entities;

namespace GameForum.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostInListViewModel>()
                .ReverseMap();
        }
    }
}
