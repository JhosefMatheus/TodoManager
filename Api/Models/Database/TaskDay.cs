namespace Api.Models.Database;

public class TaskDayEntity
{
    public int TaskId { get; set; }
    public int Day { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public TaskEntity Task { get; set; }
}