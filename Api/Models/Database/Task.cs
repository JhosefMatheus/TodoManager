using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class TaskEntity : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public int? ProjectSectionId { get; set; }
    public int TaskTypeId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Archived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ProjectEntity? Project { get; set; }
    public ProjectSectionEntity? ProjectSection { get; set; }
    public TaskTypeEntity TaskType { get; set; }
    public ICollection<TaskDayEntity> Days { get; set; }
}