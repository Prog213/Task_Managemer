using API.Models.Dtos.AppTask;

namespace API.Services.Interfaces;

public interface ITaskService
{
    public Task<IEnumerable<AppTaskDto>> GetTasksAsync(string username);
    public Task<AppTaskDto?> GetTaskByIdAsync(Guid id, string username);
    public Task<AppTaskDto> CreateTaskAsync(AddTaskDto task, string username);
    public Task UpdateTaskAsync(Guid id, UpdateTaskDto task, string username);
    public Task DeleteTaskAsync(Guid id, string username);
}
