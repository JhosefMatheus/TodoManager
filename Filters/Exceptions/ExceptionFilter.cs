using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Shared;
using TodoManager.Services.Filters.Exceptions;

namespace TodoManager.Filters.Exceptions;

public class ExceptionFilter : IExceptionFilter, IOrderedFilter
{
    public int Order => int.MaxValue;

    public void OnException(ExceptionContext context)
    {
        ExceptionFilterResponse exceptionFilterResponse;

        ExceptionFilterService? exceptionFilterService = context
            .HttpContext
            .RequestServices
            .GetService<ExceptionFilterService>();

        if (exceptionFilterService == null)
        {
            exceptionFilterResponse = new ExceptionFilterResponse
            {
                Message = "Erro inesperado no servidor.\n\nNão foi possível encontrar o serviço de tratamento de exceções.",
                Variant = AlertVariant.Error
            };
        }
        else
        {
            exceptionFilterResponse = exceptionFilterService.HandleException(context.Exception);
        }

        object response = exceptionFilterResponse.ToJson();

        context.Result = new JsonResult(response)
        {
            StatusCode = 500
        };
    }
}