using Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Tests.Utils;

public class TodoManagerContextUtils
{
    public TodoManagerContextUtils() { }

    public static TodoManagerContext GetTodoManagerContext()
    {
        DbContextOptions<TodoManagerContext> dbContextOptions = TodoManagerContext
            .CreateDbContextOptions();

        TodoManagerContext todoManagerContext = new TodoManagerContext(dbContextOptions);

        return todoManagerContext;
    }
}