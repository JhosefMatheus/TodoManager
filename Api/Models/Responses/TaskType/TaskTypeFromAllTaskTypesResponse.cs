using Api.Models.Interfaces;

namespace Api.Models.Responses.TaskType;

public class TaskTypeFromAllTaskTypesResponse : IJsonSerializable
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public object ToJson()
    {
        return new
        {
            id = Id,
            name = Name,
        };
    }
}