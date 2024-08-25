using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Api.Database;
using Api.Models.Database;
using Api.Models.Responses.Loggers.Exceptions.Filters;
using Api.Models.Shared;

namespace Api.Services.Loggers.Exceptions.Filters;

public class ExceptionFilterLoggerService
{
    private readonly TodoManagerContext todoManagerContext;

    public ExceptionFilterLoggerService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public async Task<LogExceptionResponse> LogException(Exception exception, HttpRequest request)
    {
        string requestEndpoint = request.Path;
        string requestMethod = request.Method;
        string requestHeadersText = GetRequestHeaderAsText(request);
        string requestBodyText = await GetRequestBodyAsText(request);

        string exceptionMessage = exception.Message;
        string exceptionStackTrace = exception.StackTrace ?? "";

        using IDbContextTransaction todoManagerTransaction = this.todoManagerContext.Database.BeginTransaction();

        bool exceptionSuccessfullyLogged;

        try
        {
            ExceptionLog exceptionLog = new ExceptionLog
            {
                Endpoint = requestEndpoint,
                Method = requestMethod,
                Header = requestHeadersText,
                Body = requestBodyText,
                ErrorMessage = exceptionMessage,
                StackTrace = exceptionStackTrace,
            };

            this.todoManagerContext.ExceptionLogs.Add(exceptionLog);
            this.todoManagerContext.SaveChanges();

            todoManagerTransaction.Commit();

            exceptionSuccessfullyLogged = true;
        }
        catch (Exception)
        {
            todoManagerTransaction.Rollback();

            exceptionSuccessfullyLogged = false;
        }

        LogExceptionResponse response = new LogExceptionResponse
        {
            Message = exceptionSuccessfullyLogged
                ? "Exceção registrada com sucesso no banco de dados. Olhe o registro de exceções para mais informações."
                : "Não foi possível registrar a exceção no banco de dados.",
            Variant = AlertVariant.Success,
            SuccessfullyLogged = exceptionSuccessfullyLogged,
        };

        return response;
    }

    private string GetRequestHeaderAsText(HttpRequest request)
    {
        Dictionary<string, string> requestHeaders = request
            .Headers
            .ToDictionary(
                (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> header) => header.Key,
                (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> header) => header.Value.ToString());



        string requestHeadersText = JsonConvert.SerializeObject(requestHeaders);

        return requestHeadersText;
    }

    private async Task<string> GetRequestBodyAsText(HttpRequest request)
    {
        string requestBodyText;

        request.EnableBuffering();
        request.Body.Position = 0;

        using StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        string requestBody = await reader.ReadToEndAsync();
        requestBodyText = JsonConvert.SerializeObject(requestBody);
        request.Body.Position = 0;

        return requestBodyText;
    }
}