using Api.Models.Response;

namespace Api.Models.Responses.TaskType;

public class GetAllTaskTypesResponse : BaseResponse
{
    public required List<TaskTypeFromAllTaskTypesResponse> TaskTypes { get; set; }

    public override object ToJson()
    {
        return new
        {
            message = Message,
            variant = Variant.ToString(),
            taskTypes = TaskTypes
                .Select<TaskTypeFromAllTaskTypesResponse, object>((TaskTypeFromAllTaskTypesResponse taskType) => taskType.ToJson()),
        };
    }
}