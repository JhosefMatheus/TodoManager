using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class ProjectEntity : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Archived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<ProjectSectionEntity> ProjectSections { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; }
}