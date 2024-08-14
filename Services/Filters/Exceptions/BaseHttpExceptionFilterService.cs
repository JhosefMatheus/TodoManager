using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Interfaces.Filters.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Services.Loggers.Exceptions.Filters;

namespace TodoManager.Services.Filters.Exceptions;

public class BaseHttpExceptionFilterService : IExceptionFilterService<BaseHttpException>
{
    private readonly ExceptionFilterLoggerService exceptionFilterLoggerService;

    public BaseHttpExceptionFilterService(ExceptionFilterLoggerService exceptionFilterLoggerService)
    {
        this.exceptionFilterLoggerService = exceptionFilterLoggerService;
    }

    public ExceptionFilterResponse HandleException(BaseHttpException exception)
    {
        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = exception.Message,
            Variant = exception.Variant,
            StatusCode = exception.Status,
        };

        return response;
    }
}