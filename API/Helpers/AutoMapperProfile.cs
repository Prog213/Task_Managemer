using API.Models;
using API.Models.Dtos;
using API.Models.Dtos.AppTask;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<AppTask, AppTaskDto>().ForMember(
            dest => dest.Priority,
            opt => opt.MapFrom(src => src.Priority.ToString())
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => src.Status.ToString())
        );
        CreateMap<UpdateTaskDto, AppTask>();
    }
}
