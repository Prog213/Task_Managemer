using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;
public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // Adding database context from appsettings.json
        services.AddDbContext<TaskManagementDbContext>
            (options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
            
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}
