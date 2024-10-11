using Api.Database;
using Api.Models.Database;
using Api.Models.Responses.TaskType;
using Api.Models.Shared;
using Api.Constants;

namespace Api.Services;

public class TaskTypeService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public TaskTypeService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public GetAllTaskTypesResponse GetAll()
    {
        List<TaskTypeFromAllTaskTypesResponse> taskTypes = todoManagerContext
            .TaskTypes
            .AsEnumerable()
            .Select<TaskTypeEntity, TaskTypeFromAllTaskTypesResponse>((TaskTypeEntity taskType) =>
            {
                return new TaskTypeFromAllTaskTypesResponse()
                {
                    Id = taskType.Id,
                    Name = taskType.Name
                };
            })
            .ToList<TaskTypeFromAllTaskTypesResponse>();

        return new GetAllTaskTypesResponse()
        {
            Message = TaksTypeConstants.GetAllTaskTypesSuccessMessage,
            Variant = AlertVariant.Success,
            TaskTypes = taskTypes,
        };
    }
}