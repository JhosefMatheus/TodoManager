using Api.Database;
using Api.Models.DTO.Task;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace Tests.Utils;

public class DiaryTaskUtils : BaseUtils
{
    public static JObject CreateDiaryTaskTestDTO(int taskTypeId, int? projectId, int? projectSectionId)
    {
        CreateDiaryTaskDTO createDiaryTaskTestDTO = new CreateDiaryTaskDTO()
        {
            ProjectId = projectId,
            ProjectSectionId = projectSectionId,
            TaskTypeId = taskTypeId,
            Name = GetDiaryTaskTestName(),
            Description = GetDiaryTaskDescription(),
            Days = new List<int>() { 1, 2, 3, 4, 5, 6, 7 },
        };

        JObject createDiaryTaskJObject = JObject.FromObject(createDiaryTaskTestDTO);

        return createDiaryTaskJObject;
    }

    public static string GetDiaryTaskTestName()
    {
        string name = "Diary task test";

        return name;
    }

    public static string GetDiaryTaskDescription()
    {
        string description = "Diary task test description.";

        return description;
    }

    public static void ClearTaskDayTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.TaskDays.RemoveRange(todoManagerContext.TaskDays);

        todoManagerContext.SaveChanges();
    }
}