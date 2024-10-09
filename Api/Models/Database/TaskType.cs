using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class TaskType : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Task> Tasks { get; set; }
}