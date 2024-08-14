using TodoManager.Models.Exceptions;
using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Interfaces.Filters.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Shared;
using TodoManager.Services.Loggers.Exceptions.Filters;

namespace TodoManager.Services.Filters.Exceptions;

public class ExceptionFilterService : IExceptionFilterService<Exception>
{
    private readonly BaseExceptionFilterService baseExceptionFilterService;
    private readonly BaseHttpExceptionFilterService baseHttpExceptionFilterService;
    private readonly ExceptionFilterLoggerService exceptionFilterLoggerService;

    public ExceptionFilterService
    (
        BaseExceptionFilterService baseExceptionFilterService,
        BaseHttpExceptionFilterService baseHttpExceptionFilterService,
        ExceptionFilterLoggerService exceptionFilterLoggerService
    )
    {
        this.baseExceptionFilterService = baseExceptionFilterService;
        this.baseHttpExceptionFilterService = baseHttpExceptionFilterService;
        this.exceptionFilterLoggerService = exceptionFilterLoggerService;
    }

    public ExceptionFilterResponse HandleException(Exception exception)
    {
        ExceptionFilterResponse response;

        if (exception is BaseHttpException baseHttpException)
        {
            response = this.baseHttpExceptionFilterService.HandleException(baseHttpException);
        }
        else if (exception is BaseException baseException)
        {
            response = this.baseExceptionFilterService.HandleException(baseException);
        }
        else
        {
            response = new ExceptionFilterResponse
            {
                Message = "Erro inesperado no servidor. Olhe o registro de exceções para mais informações.",
                Variant = AlertVariant.Error,
                StatusCode = 500,
            };
        }

        return response;
    }
}