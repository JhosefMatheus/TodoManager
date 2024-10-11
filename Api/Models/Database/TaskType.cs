using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class TaskTypeEntity : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<TaskEntity> Tasks { get; set; }
}