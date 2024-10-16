using Api.Models.Exceptions;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Interfaces.Filters.Exceptions;
using Api.Models.Responses.Filters.Exceptions;
using Api.Models.Responses.Loggers.Exceptions.Filters;
using Api.Models.Shared;
using Api.Services.Loggers.Exceptions.Filters;

namespace Api.Services.Filters.Exceptions;

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

    public async Task<ExceptionFilterResponse> HandleException(Exception exception, HttpRequest request)
    {
        ExceptionFilterResponse response;

        if (exception is BaseHttpException baseHttpException)
        {
            response = await this.baseHttpExceptionFilterService.HandleException(baseHttpException, request);
        }
        else if (exception is BaseException baseException)
        {
            response = await this.baseExceptionFilterService.HandleException(baseException, request);
        }
        else
        {
            LogExceptionResponse logExceptionResponse = await this.exceptionFilterLoggerService.LogException(exception, request);

            response = new ExceptionFilterResponse()
            {
                Message = $"Erro inesperado no servidor. {logExceptionResponse.Message}",
                Variant = AlertVariant.Error,
                StatusCode = 500,
            };
        }

        return response;
    }
}