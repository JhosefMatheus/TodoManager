using TodoManager.Models.Exceptions;
using TodoManager.Models.Interfaces.Filters.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Services.Loggers.Exceptions.Filters;

namespace TodoManager.Services.Filters.Exceptions;

public class BaseExceptionFilterService : IExceptionFilterService<BaseException>
{
    private readonly ExceptionFilterLoggerService exceptionFilterLoggerService;

    public BaseExceptionFilterService(ExceptionFilterLoggerService exceptionFilterLoggerService)
    {
        this.exceptionFilterLoggerService = exceptionFilterLoggerService;
    }

    public async Task<ExceptionFilterResponse> HandleException(BaseException exception, HttpRequest request)
    {
        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = exception.Message,
            Variant = exception.Variant,
            StatusCode = 500,
        };

        return response;
    }
}