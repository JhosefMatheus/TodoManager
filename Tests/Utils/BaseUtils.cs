using Api.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Utils;

public abstract class BaseUtils
{
    public static TodoManagerContext GetTodoManagerContext(ServiceProvider serviceProvider)
    {
        TodoManagerContext todoManagerContext = serviceProvider.GetService<TodoManagerContext>()
            ?? throw new NullReferenceException("Não foi possível encontrar o contexto do banco de dados.");

        return todoManagerContext;
    }
}