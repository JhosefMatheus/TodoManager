using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Shared;

namespace TodoManager.Services.Filters.Exceptions;

public class ExceptionFilterService
{
    public ExceptionFilterResponse HandleException(Exception exception)
    {
        ExceptionFilterResponse response = new ExceptionFilterResponse
        {
            Message = "Erro inesperado no servidor. Olhe o registro de exceções para mais informações.",
            Variant = AlertVariant.Error,
        };

        return response;
    }
}