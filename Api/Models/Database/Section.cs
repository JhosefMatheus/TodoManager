namespace Api.Models.Database;

public class Section
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Project Project { get; set; }
}