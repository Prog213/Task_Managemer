using API.Helpers;
using API.Models;
using API.Models.Dtos.AppTask;

namespace API.Services.Interfaces;

public interface ITaskService
{
    public Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams);
    public Task<AppTaskDto?> GetTaskDtoByIdAsync(Guid id, string username);
    public Task<AppTask> GetTaskByIdAsync(Guid id, string username);
    public Task<AppTaskDto> CreateTaskAsync(AddTaskDto task, string username);
    public Task UpdateTaskAsync(Guid id, UpdateTaskDto task, string username);
    public Task DeleteTaskAsync(Guid id, string username);
}
