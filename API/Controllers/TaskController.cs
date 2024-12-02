using API.Extensions;
using API.Models.Dtos.AppTask;
using API.Services.Interfaces;
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
        public async Task<ActionResult<AppTaskDto>> GetTasks()
        {
            var tasks = await taskService.GetTasksAsync(User.GetUsername());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var task = await taskService.GetTaskByIdAsync(id, User.GetUsername());
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(AddTaskDto taskDto)
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
