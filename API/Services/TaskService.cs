using API.Helpers;
using API.Models;
using API.Models.Dtos.AppTask;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class TaskService(ITaskRepository taskRepository, IUserService userService, IMapper mapper, 
    ILogger<TaskService> logger) : ITaskService
{
    public async Task<AppTaskDto> CreateTaskAsync(AddTaskDto taskDto, string username)
    {
        logger.LogInformation("Creating new task: {@task} for user {username}", taskDto, username);

        var user = await userService.GetUserByUsernameAsync(username);

        var task = new AppTask
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            Status = Enum.Parse<AppTaskStatus>(taskDto.Status),
            Priority = Enum.Parse<AppTaskPriority>(taskDto.Priority),
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        taskRepository.AddTask(task);
        await taskRepository.SaveAllAsync();

        return mapper.Map<AppTaskDto>(task);
    }

    public async Task DeleteTaskAsync(Guid id, string username)
    {
        logger.LogInformation("Deleting task with id: {id} for user {username}", id, username);

        var existingTask = await GetTaskByIdAsync(id, username);

        var user = await userService.GetUserByUsernameAsync(username);

        if (existingTask!.User == null || existingTask.UserId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only update your own tasks.");
        }

        taskRepository.DeleteTask(existingTask);
        await taskRepository.SaveAllAsync();
    }

    public async Task<AppTask> GetTaskByIdAsync(Guid id, string username)
    {
        var existingTask = await taskRepository.GetTaskByIdAsync(id, username) 
            ?? throw new KeyNotFoundException($"Task with id:{id} is not found.");
        
        return existingTask;
    }

    public async Task<AppTaskDto?> GetTaskDtoByIdAsync(Guid id, string username)
    {
        var task = await GetTaskByIdAsync(id, username);
        return mapper.Map<AppTaskDto>(task);
    }

    public async Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams)
    {
        var tasksPagedList = await taskRepository.GetTasksAsync(username, queryParams);
        return tasksPagedList;
    }

    public async Task UpdateTaskAsync(Guid id, UpdateTaskDto task, string username)
    {
        logger.LogInformation("Updating task with id: {id} for user {username}", id, username);
        
        var existingTask = await GetTaskByIdAsync(id, username);

        var user = await userService.GetUserByUsernameAsync(username);

        if (existingTask.User == null || existingTask.UserId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only update your own tasks.");
        }

        existingTask.UpdatedAt = DateTime.Now;
        mapper.Map(task, existingTask);
        taskRepository.UpdateTask(existingTask);

        await taskRepository.SaveAllAsync();
    }
}
