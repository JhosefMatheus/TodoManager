using Api.Database;
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
}