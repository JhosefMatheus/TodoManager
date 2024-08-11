using TodoManager.Models.Interfaces;

namespace TodoManager.Models.Responses.Project;

public class ProjectByIdResponse : IJsonSerializable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public object ToJson()
    {
        return new
        {
            id = Id,
            name = Name,
            createdAt = CreatedAt,
            updatedAt = UpdatedAt,
        };
    }
}