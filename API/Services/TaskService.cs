using API.Helpers;
using API.Models;
using API.Models.Dtos.AppTask;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services;

public class TaskService(ITaskRepository taskRepository, IUserService userService, IMapper mapper) : ITaskService
{
    public async Task<AppTaskDto> CreateTaskAsync(AddTaskDto taskDto, string username)
    {
        var user = await userService.GetUserByUsernameAsync(username) 
            ?? throw new UnauthorizedAccessException("User not found");

        var task = new AppTask
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            Status = taskDto.Status,
            Priority = taskDto.Priority,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        taskRepository.AddTask(task);
        await taskRepository.SaveAllAsync();

        return mapper.Map<AppTaskDto>(task);
    }

    public async Task DeleteTaskAsync(Guid id, string username)
    {
        var existingTask = await taskRepository.GetTaskByIdAsync(id, username) 
            ?? throw new KeyNotFoundException("Task not found.");

        var user = await userService.GetUserByUsernameAsync(username);

        if (existingTask.User == null || existingTask.UserId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only delete your own tasks.");
        }

        taskRepository.DeleteTask(existingTask);
        await taskRepository.SaveAllAsync();
    }

    public async Task<AppTaskDto?> GetTaskByIdAsync(Guid id, string username)
    {
        var task = await taskRepository.GetTaskByIdAsync(id, username)
            ?? throw new KeyNotFoundException("Task not found.");

        return mapper.Map<AppTaskDto>(task);
    }

    public async Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams)
    {
        var tasksPagedList = await taskRepository.GetTasksAsync(username, queryParams);
        return tasksPagedList;
    }

    public async Task UpdateTaskAsync(Guid id, UpdateTaskDto task, string username)
    {
        var existingTask = await taskRepository.GetTaskByIdAsync(id, username) 
            ?? throw new KeyNotFoundException("Task not found.");

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
