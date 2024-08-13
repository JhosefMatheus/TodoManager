using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Responses.Filters.Exceptions;

namespace TodoManager.Services.Filters.Exceptions;

public class BaseHttpExceptionFilterService
{
    public ExceptionFilterResponse HandleException(BaseHttpException exception)
    {
        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = exception.Message,
            Variant = exception.Variant,
        };

        return response;
    }
}