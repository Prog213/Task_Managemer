using API.Models;

namespace API.Repositories.Interfaces;

public interface ITaskRepository
{
    public Task<IEnumerable<AppTask>> GetTasksAsync(string username);
    public Task<AppTask?> GetTaskByIdAsync(Guid id, string username);
    public void AddTask(AppTask task);
    public void UpdateTask(AppTask task);
    public void DeleteTask(AppTask task);
    public Task<bool> SaveAllAsync();
}
