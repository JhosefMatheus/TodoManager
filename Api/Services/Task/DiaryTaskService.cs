using Api.Database;
using Api.Models.DTO.Task;
using Api.Models.Interfaces.Services;
using Api.Models.Responses.Task;
using Api.Models.Shared;

namespace Api.Services.Task;

public class DiaryTaskService : BaseService, ITaskService
{
    private readonly TodoManagerContext todoManagerContext;

    public DiaryTaskService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public CreateTaskResponse Create(BaseCreateTaskDTO baseCreateTaskDTO)
    {
        CreateDiaryTaskDTO.FromBaseCreateTaskDTO(baseCreateTaskDTO);

        return new CreateTaskResponse()
        {
            Message = "Tarefa criado com sucesso.",
            Variant = AlertVariant.Success,
        };
    }
}