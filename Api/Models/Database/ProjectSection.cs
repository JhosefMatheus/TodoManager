namespace Api.Models.Database;

public class ProjectSection
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Project Project { get; set; }
}