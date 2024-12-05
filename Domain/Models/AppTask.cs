using Domain.Enums;

namespace Domain.Models;

public class AppTask
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public AppTaskStatus Status { get; set; }
    public AppTaskPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid? UserId { get; set; }
    public User? User { get; set; }
}
