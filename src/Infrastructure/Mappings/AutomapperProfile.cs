using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Infrastructure.Mappings;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Post, PostDto>();
        CreateMap<PostDto, Post>();
    }
}