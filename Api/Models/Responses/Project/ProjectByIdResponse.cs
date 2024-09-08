using Api.Models.Interfaces;

namespace Api.Models.Responses.Project;

public class ProjectByIdResponse : IJsonSerializable
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required bool Archived { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }

    public object ToJson()
    {
        return new
        {
            id = Id,
            name = Name,
            archived = Archived,
            createdAt = CreatedAt,
            updatedAt = UpdatedAt,
        };
    }
}