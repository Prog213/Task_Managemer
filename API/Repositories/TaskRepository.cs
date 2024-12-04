using API.Data;
using API.Helpers;
using API.Models;
using API.Models.Dtos.AppTask;
using API.Repositories.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class TaskRepository(TaskManagementDbContext context, IMapper mapper) : ITaskRepository
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

    public async Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams)
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

        return await PagedList<AppTaskDto>.CreateAsync(mapper.ProjectTo<AppTaskDto>(query), queryParams.PageNumber, queryParams.PageSize);
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
