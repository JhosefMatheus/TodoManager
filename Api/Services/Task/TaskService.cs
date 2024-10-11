using Api.Database;

namespace Api.Services.Task;

public class TaskService : BaseService
{
    private readonly TodoManagerContext todoManagerContext;

    public TaskService(TodoManagerContext todoManagerContext, DiaryTaskService diaryTaskService)
    {
        this.todoManagerContext = todoManagerContext;
    }
}