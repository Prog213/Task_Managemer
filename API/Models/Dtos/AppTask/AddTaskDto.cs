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
    [EnumDataType(typeof(AppTaskStatus), ErrorMessage = "Invalid status. Valid values are: Pending, InProgress, Completed.")]
    public string Status { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(AppTaskPriority), ErrorMessage = "Invalid priority. Valid values are: Low, Medium, High.")]
    public string Priority { get; set; } = null!;
}
