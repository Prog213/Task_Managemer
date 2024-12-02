using System.ComponentModel.DataAnnotations;
using API.Models.Enums;

namespace API.Models.Dtos.AppTask;

public class UpdateTaskDto
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    [Required]
    public AppTaskStatus Status { get; set; }

    [Required]
    public AppTaskPriority Priority { get; set; }
}
