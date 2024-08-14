using TodoManager.Models.Responses.Filters.Exceptions;

namespace TodoManager.Models.Interfaces.Filters.Exceptions;

public interface IExceptionFilterService<T> where T : Exception
{
    public ExceptionFilterResponse HandleException(T exception);
}