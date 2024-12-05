using API.Extensions;
using Application.Common;
using Application.Dtos.AppTask;
using Application.Interfaces;
using Domain.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/tasks")]
    [Authorize]
    [ApiController]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<AppTaskDto>>> GetTasks([FromQuery] TaskQueryParams queryParams)
        {
            var tasks = await taskService.GetTasksAsync(User.GetUsername(), queryParams);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppTaskDto>> GetTaskById(Guid id)
        {
            var task = await taskService.GetTaskByIdAsync(id, User.GetUsername());
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<AppTaskDto>> CreateTask(AddTaskDto taskDto)
        {
            var task = await taskService.CreateTaskAsync(taskDto, User.GetUsername());
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto taskDto)
        {
            await taskService.UpdateTaskAsync(id, taskDto, User.GetUsername());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await taskService.DeleteTaskAsync(id, User.GetUsername());
            return NoContent();
        }
    }
}
