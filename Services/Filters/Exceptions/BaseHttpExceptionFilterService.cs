using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Interfaces.Filters.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Responses.Loggers.Exceptions.Filters;
using TodoManager.Services.Loggers.Exceptions.Filters;

namespace TodoManager.Services.Filters.Exceptions;

public class BaseHttpExceptionFilterService : IExceptionFilterService<BaseHttpException>
{
    private readonly ExceptionFilterLoggerService exceptionFilterLoggerService;

    public BaseHttpExceptionFilterService(ExceptionFilterLoggerService exceptionFilterLoggerService)
    {
        this.exceptionFilterLoggerService = exceptionFilterLoggerService;
    }

    public async Task<ExceptionFilterResponse> HandleException(BaseHttpException exception, HttpRequest request)
    {
        string responseMessage = exception.Message;

        if (exception.Status == 500)
        {
            LogExceptionResponse logExceptionResponse = await this.exceptionFilterLoggerService.LogException(exception, request);

            responseMessage += $" {logExceptionResponse.Message}";
        }


        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = responseMessage,
            Variant = exception.Variant,
            StatusCode = exception.Status,
        };

        return response;
    }
}