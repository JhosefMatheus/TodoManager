using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Api.Models.Responses.Filters.Exceptions;
using Api.Models.Shared;
using Api.Services.Filters.Exceptions;

namespace Api.Filters.Exceptions;

public class ExceptionFilter : IExceptionFilter, IOrderedFilter
{
    public int Order => int.MaxValue;

    public async void OnException(ExceptionContext context)
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
                Variant = AlertVariant.Error,
                StatusCode = 500,
            };
        }
        else
        {
            HttpRequest request = context.HttpContext.Request;

            exceptionFilterResponse = await exceptionFilterService.HandleException(context.Exception, request);
        }

        object response = exceptionFilterResponse.ToJson();

        context.Result = new JsonResult(response)
        {
            StatusCode = exceptionFilterResponse.StatusCode,
        };
    }
}