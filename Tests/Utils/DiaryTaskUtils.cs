using Api.Database;
using Api.Models.DTO.Task;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class DiaryTaskUtils : BaseUtils
{
    public static CreateDiaryTaskDTO CreateDiaryTaskTestDTO(int? projectId, int? projectSectionId)
    {
        CreateDiaryTaskDTO createDiaryTaskTestDTO = new CreateDiaryTaskDTO()
        {
            ProjectId = projectId,
            ProjectSectionId = projectSectionId,
            Name = "Diary task test",
            Description = "Diary task test description.",
            Days = new List<int>() { 1, 2, 3, 4, 5, 6, 7 },
        };

        return createDiaryTaskTestDTO;
    }

    public static void ClearTaskDayTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.TaskDays.RemoveRange(todoManagerContext.TaskDays);

        todoManagerContext.SaveChanges();
    }
}