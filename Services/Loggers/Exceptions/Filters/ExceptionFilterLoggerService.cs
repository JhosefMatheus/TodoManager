using TodoManager.Database;
using TodoManager.Models.Responses.Loggers.Exceptions.Filters;
using TodoManager.Models.Shared;

namespace TodoManager.Services.Loggers.Exceptions.Filters;

public class ExceptionFilterLoggerService
{
    private readonly TodoManagerContext todoManagerContext;

    public ExceptionFilterLoggerService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public LogExceptionResponse LogException(Exception exception)
    {
        LogExceptionResponse response = new LogExceptionResponse
        {
            Message = "Exceção registrada com sucesso.",
            Variant = AlertVariant.Success,
            SuccessfullyLogged = true,
        };

        return response;
    }
}