using Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Tests.Utils;

public class TodoManagerContextUtils
{
    private static readonly string AppSettingsJsonPath = "../../Api/appsettings.Development.json";

    public TodoManagerContextUtils() { }

    public static TodoManagerContext GetTodoManagerContext()
    {
        DbContextOptions<TodoManagerContext> dbContextOptions = TodoManagerContext
            .CreateDbContextOptions(AppSettingsJsonPath);

        TodoManagerContext todoManagerContext = new TodoManagerContext(dbContextOptions);

        return todoManagerContext;
    }
}