using Application.Dtos;
using Application.Dtos.AppTask;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Create maps for models and DTOs
        CreateMap<User, UserDto>();

        // Map enum values to strings
        CreateMap<AppTask, AppTaskDto>().ForMember(
            dest => dest.Priority,
            opt => opt.MapFrom(src => src.Priority.ToString())
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => src.Status.ToString())
        );
        CreateMap<UpdateTaskDto, AppTask>()
            .ForMember(
            dest => dest.DueDate,
            opt => opt.MapFrom(src => src.DueDate!.Value.ToUniversalTime())
        );
    }
}
