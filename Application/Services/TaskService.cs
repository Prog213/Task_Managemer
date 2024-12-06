using Application.Common;
using Application.Dtos.AppTask;
using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Specification;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class TaskService(ITaskRepository taskRepository, IUserService userService, IMapper mapper,
    ILogger<TaskService> logger) : ITaskService
{
    public async Task<AppTaskDto> CreateTaskAsync(AddTaskDto taskDto, string username)
    {
        logger.LogInformation("Creating new task: {@task} for user {username}", taskDto, username);

        // Getting user by username
        var user = await userService.GetUserByUsernameAsync(username);

        //Creating a new task from the DTO
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

        // Add task to the repository and save changes
        taskRepository.AddTask(task);
        await taskRepository.SaveAllAsync();

        return mapper.Map<AppTaskDto>(task);
    }

    public async Task DeleteTaskAsync(Guid id, string username)
    {
        logger.LogInformation("Deleting task with id: {id} for user {username}", id, username);

        //Getting the existing task by id
        var existingTask = await GetTaskByIdAsync(id, username);

        // Validating user ownership
        await ValidateUserOwnership(existingTask, username);

        // Deleting the task and saving changes
        taskRepository.DeleteTask(existingTask);
        await taskRepository.SaveAllAsync();
    }

    public async Task<AppTask> GetTaskByIdAsync(Guid id, string username)
    {
        // Getting the task by id and username
        var existingTask = await taskRepository.GetTaskByIdAsync(id, username)
            ?? throw new KeyNotFoundException($"Task with id:{id} is not found.");

        return existingTask;
    }

    public async Task<AppTaskDto?> GetTaskDtoByIdAsync(Guid id, string username)
    {
        // Getting the task by id and username and mapping it to DTO
        var task = await GetTaskByIdAsync(id, username);
        return mapper.Map<AppTaskDto>(task);
    }

    public async Task<PagedList<AppTaskDto>> GetTasksAsync(string username, TaskQueryParams queryParams)
    {
        // Getting tasks by username and query parameters
        (var tasks , int itemCount) = await taskRepository.GetTasksAsync(username, queryParams);

        // Mapping tasks to DTOs and returning a paged list
        var taskDtos = mapper.Map<IEnumerable<AppTaskDto>>(tasks);
        return new PagedList<AppTaskDto>(taskDtos, itemCount, queryParams.PageNumber, queryParams.PageSize);
    }

    public async Task UpdateTaskAsync(Guid id, UpdateTaskDto task, string username)
    {
        logger.LogInformation("Updating task with id: {id} for user {username}", id, username);

        // Getting the existing task by id
        var existingTask = await GetTaskByIdAsync(id, username);

        // Validating user ownership
        await ValidateUserOwnership(existingTask, username);

        // Updating the task, updating the UpdatedAt property time and saving changes
        mapper.Map(task, existingTask);
        taskRepository.UpdateTask(existingTask);

        existingTask.UpdatedAt = DateTime.Now.ToUniversalTime();
        await taskRepository.SaveAllAsync();
    }

    public async Task ValidateUserOwnership(AppTask existingTask, string username)
    {
        // Getting user by username
        var user = await userService.GetUserByUsernameAsync(username);
        // Checking if the task belongs to the user
        if (existingTask.User == null || existingTask.UserId != user.Id)
        {
            throw new UnauthorizedAccessException("You can only modify your own tasks.");
        }
    }
}
