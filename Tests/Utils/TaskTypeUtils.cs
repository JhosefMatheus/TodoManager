using Api.Database;
using Api.Models.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class TaskTypeUtils : BaseUtils
{
    public static void ClearTaskTypesTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.TaskTypes.RemoveRange(todoManagerContext.TaskTypes);

        todoManagerContext.SaveChanges();
    }

    public static void PopulateTaskTypesTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        TaskTypeEntity DiaryTaskType = new TaskTypeEntity()
        {
            Name = "Diária"
        };

        todoManagerContext.TaskTypes.Add(DiaryTaskType);

        todoManagerContext.SaveChanges();
    }
}