using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Responses.Filters.Exceptions;
using TodoManager.Models.Shared;
using TodoManager.Services.Filters.Exceptions;

namespace TodoManager.Filters.Exceptions;

public class BaseHttpExceptionFilter : IExceptionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 2;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is BaseHttpException exception)
        {
            ExceptionFilterResponse exceptionFilterResponse;

            BaseHttpExceptionFilterService? baseHttpExceptionFilterService = context
                .HttpContext
                .RequestServices
                .GetService<BaseHttpExceptionFilterService>();

            if (baseHttpExceptionFilterService == null)
            {
                exceptionFilterResponse = new ExceptionFilterResponse
                {
                    Message = "Erro inesperado no servidor.\n\nNão foi possível encontrar o serviço de tratamento de exceções.",
                    Variant = AlertVariant.Error
                };
            }
            else
            {
                exceptionFilterResponse = baseHttpExceptionFilterService.HandleException(exception);
            }

            object response = exceptionFilterResponse.ToJson();

            context.Result = new JsonResult(response)
            {
                StatusCode = exception.Status,
            };
        }
    }
}