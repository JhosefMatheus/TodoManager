using Api.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public class SectionUtils : BaseUtils
{
    public static void ClearProjectsTable(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = GetTodoManagerContext(serviceProvider);

        todoManagerContext.Sections.RemoveRange(todoManagerContext.Sections);

        todoManagerContext.SaveChanges();
    }
}