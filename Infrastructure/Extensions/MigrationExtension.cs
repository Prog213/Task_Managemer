using System;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        // Create a scope to get the DbContext
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<TaskManagementDbContext>();

        // Apply the migrations if there are any pending
        if (context!.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
    