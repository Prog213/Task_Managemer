using Application.Dtos;
using Application.Dtos.AppTask;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper;

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
