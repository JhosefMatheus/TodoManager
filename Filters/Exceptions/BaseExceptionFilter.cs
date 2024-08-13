using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoManager.Models.Exceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Shared;
using TodoManager.Services.Filters.Exceptions;

namespace TodoManager.Filters.Exceptions;

public class BaseExceptionFilter : IExceptionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 1;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseException exception)
        {
            ExceptionFilterResponse exceptionFilterResponse;

            BaseExceptionFilterService? baseExceptionFilterService = context
                .HttpContext
                .RequestServices
                .GetService<BaseExceptionFilterService>();

            if (baseExceptionFilterService == null)
            {
                exceptionFilterResponse = new ExceptionFilterResponse
                {
                    Message = "Erro inesperado no servidor.\n\nNão foi possível encontrar o serviço de tratamento de exceções.",
                    Variant = AlertVariant.Error
                };
            }
            else
            {
                exceptionFilterResponse = baseExceptionFilterService.HandleException(exception);
            }

            object response = exceptionFilterResponse.ToJson();

            context.Result = new JsonResult(response)
            {
                StatusCode = 500,
            };
        }
    }
}