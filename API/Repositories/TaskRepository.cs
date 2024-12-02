using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

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

    public async Task<IEnumerable<AppTask>> GetTasksAsync(string username)
    {
        return await context.Tasks.Where(t => t.User!.Username == username).ToListAsync();
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
