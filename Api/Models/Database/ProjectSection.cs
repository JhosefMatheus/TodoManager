using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class ProjectSectionEntity : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public bool Archived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ProjectEntity Project { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; }
}