using API.Helpers;
using API.Models;
using API.Models.Dtos.AppTask;

namespace API.Repositories.Interfaces;

public interface ITaskRepository
{
    public Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams);
    public Task<AppTask?> GetTaskByIdAsync(Guid id, string username);
    public void AddTask(AppTask task);
    public void UpdateTask(AppTask task);
    public void DeleteTask(AppTask task);
    public Task<bool> SaveAllAsync();
}
