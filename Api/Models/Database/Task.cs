using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class Task : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public int? ProjectSectionId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool Archived { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Project? Project { get; set; }
    public ProjectSection? ProjectSection { get; set; }
}