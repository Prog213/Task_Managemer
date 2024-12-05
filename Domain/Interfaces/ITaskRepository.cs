using Domain.Models;
using Domain.Specification;

namespace Domain.Interfaces;

public interface ITaskRepository
{
    public Task<(IEnumerable<AppTask>, int)> GetTasksAsync(string username, TaskQueryParams queryParams);
    public Task<AppTask?> GetTaskByIdAsync(Guid id, string username);
    public void AddTask(AppTask task);
    public void UpdateTask(AppTask task);
    public void DeleteTask(AppTask task);
    public Task<bool> SaveAllAsync();
}
