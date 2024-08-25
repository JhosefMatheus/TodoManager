using Api.Models.Responses.Filters.Exceptions;

namespace Api.Models.Interfaces.Filters.Exceptions;

public interface IExceptionFilterService<T> where T : Exception
{
    public Task<ExceptionFilterResponse> HandleException(T exception, HttpRequest request);
}