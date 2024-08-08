using TodoManager.Models.Database;

namespace TodoManager.Database;

public class ProjectType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Project> Projects { get; set; }
}