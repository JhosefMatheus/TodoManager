using TodoManager.Models.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;

namespace TodoManager.Services.Filters.Exceptions;

public class BaseExceptionFilterService
{
    public ExceptionFilterResponse HandleException(BaseException exception)
    {
        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = exception.Message,
            Variant = exception.Variant,
        };

        return response;
    }
}