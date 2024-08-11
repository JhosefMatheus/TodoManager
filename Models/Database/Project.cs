using TodoManager.Database;

namespace TodoManager.Models.Database;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}