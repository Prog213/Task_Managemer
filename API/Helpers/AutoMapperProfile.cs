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
        CreateMap<AppTask, AppTaskDto>();
        CreateMap<UpdateTaskDto, AppTask>();
    }
}
