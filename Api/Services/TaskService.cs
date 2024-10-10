using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Responses.Task;
using Api.Constants;
using Api.Models.Shared;
using Api.Services.Task;

namespace Api.Services;

public class TaskService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;
    private readonly DiaryTaskService diaryTaskService;

    public TaskService(TodoManagerContext todoManagerContext, DiaryTaskService diaryTaskService)
    {
        this.todoManagerContext = todoManagerContext;
        this.diaryTaskService = diaryTaskService;
    }

    public CreateTaskResponse Create(BaseCreateTaskDTO createTaskDTO)
    {
        TaskType taskType = FindById<TaskType>(todoManagerContext.TaskTypes, createTaskDTO.TaskTypeId)
            ?? throw new NotFoundHttpException(TaksTypeConstants.TaskTypeNotFoundMessage, AlertVariant.Error);

        CreateTaskResponse createTaskResponse;

        switch (taskType.Name)
        {
            case "Diária":
                createTaskResponse = diaryTaskService.Create(createTaskDTO);
                break;
            default:
                throw new InternalServerErrorHttpException
                (
                    TaksTypeConstants.TaskTypeNameNotFoundMessage,
                    AlertVariant.Error
                );
        }

        return createTaskResponse;
    }
}