using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.AppTask;

public class UpdateTaskDto
{
    [Required]
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    [Required]
    [EnumDataType(typeof(AppTaskStatus), ErrorMessage = "Invalid status. Valid values are: Pending, InProgress, Completed.")]
    public string Status { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(AppTaskPriority), ErrorMessage = "Invalid priority. Valid values are: Low, Medium, High.")]
    public string Priority { get; set; } = null!;
}
