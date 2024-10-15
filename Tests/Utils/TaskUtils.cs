using Api.Database;
using Api.Models.Database;
using Api.Models.DTO.Task;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class TaskUtils : BaseUtils
{
    public static void ClearTaskTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.Tasks.RemoveRange(todoManagerContext.Tasks);

        todoManagerContext.SaveChanges();
    }

    public static MoveTaskToDTO CreateMoveTaskToTestDTO(int? projectId, int? projectSectionId)
    {
        MoveTaskToDTO moveTaskToTestDTO = new MoveTaskToDTO()
        {
            ProjectId = projectId,
            ProjectSectionId = projectSectionId,
        };

        return moveTaskToTestDTO;
    }

    public static UpdateTaskDTO CreateUpdateTaskTestDTO(string? name, string? description)
    {
        UpdateTaskDTO updateTaskTestDTO = new UpdateTaskDTO()
        {
            Name = name ?? DiaryTaskUtils.GetDiaryTaskTestName(),
            Description = description
        };

        return updateTaskTestDTO;
    }

    public static TaskEntity GetFirstEntity(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        TaskEntity taskEntity = todoManagerContext.Tasks.First();

        return taskEntity;
    }
}