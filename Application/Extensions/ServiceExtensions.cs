using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;
public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        // Add application services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddAutoMapper(typeof(ServiceExtensions).Assembly);
    }
}
