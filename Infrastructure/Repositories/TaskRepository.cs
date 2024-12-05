using Domain.Interfaces;
using Domain.Models;
using Domain.Specification;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskRepository(TaskManagementDbContext context) : ITaskRepository
{
    public void AddTask(AppTask task)
    {
        context.Tasks.Add(task);
    }

    public void DeleteTask(AppTask task)
    {
        context.Tasks.Remove(task);
    }

    public async Task<AppTask?> GetTaskByIdAsync(Guid id, string username)
    {
        return await context.Tasks.Where(t => t.User!.Username == username)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<(IEnumerable<AppTask>, int)> GetTasksAsync(string username, TaskQueryParams queryParams)
    {
        var query = context.Tasks
            .Where(t => t.User!.Username == username)
            .AsQueryable();

        if (queryParams.Status.HasValue)
        {
            query = query.Where(t => t.Status == queryParams.Status.Value);
        }

        if (queryParams.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == queryParams.Priority.Value);
        }

        if (queryParams.MinDate.HasValue)
        {
            var date = queryParams.MinDate.Value.ToUniversalTime();
            query = query.Where(t => t.DueDate >= date);
        }

        if (queryParams.MaxDate.HasValue)
        {
            var date = queryParams.MaxDate.Value.ToUniversalTime();
            query = query.Where(t => t.DueDate <= date);
        }

        query = queryParams.OrderBy switch
        {
            "DueDate" => query.OrderBy(t => t.DueDate),
            "Priority" => query.OrderBy(t => t.Priority),
            _ => query.OrderBy(t => t.Title)
        };

        var result = await query.ToListAsync();
        var count = await query.CountAsync();

        return (result, count);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateTask(AppTask task)
    {
        context.Tasks.Entry(task).State = EntityState.Modified;
    }
}
