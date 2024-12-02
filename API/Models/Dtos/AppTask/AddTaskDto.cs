using System.ComponentModel.DataAnnotations;
using API.Models.Enums;

namespace API.Models.Dtos.AppTask;

public class AddTaskDto
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    [Required]
    public AppTaskStatus Status { get; set; }

    [Required]
    public AppTaskPriority Priority { get; set; }
}
