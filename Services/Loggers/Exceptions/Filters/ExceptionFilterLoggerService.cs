using System.Text;
using Newtonsoft.Json;
using TodoManager.Database;
using TodoManager.Models.Responses.Loggers.Exceptions.Filters;
using TodoManager.Models.Shared;

namespace TodoManager.Services.Loggers.Exceptions.Filters;

public class ExceptionFilterLoggerService
{
    private readonly TodoManagerContext todoManagerContext;

    public ExceptionFilterLoggerService(TodoManagerContext todoManagerContext)
    {
        this.todoManagerContext = todoManagerContext;
    }

    public async Task<LogExceptionResponse> LogException(Exception exception, HttpRequest request)
    {
        var headersJson = JsonConvert.SerializeObject(request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));

        // Obtenha o corpo da request como JSON
        string bodyJson;
        request.EnableBuffering(); // Habilita o buffering para permitir que o corpo seja lido mais de uma vez
        request.Body.Position = 0; // Reposiciona o stream para o início
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync(); // Lê o corpo da requisição de forma assíncrona
            bodyJson = JsonConvert.SerializeObject(body); // Converte o corpo para JSON
            request.Body.Position = 0; // Reposiciona o stream novamente, caso seja necessário em outro lugar
        }

        Console.WriteLine("Request:");
        Console.WriteLine($"\tEndpoint: {request.Path}");
        Console.WriteLine($"\tMethod: {request.Method}");
        Console.WriteLine($"\tHeader: {headersJson}");
        Console.WriteLine($"\tBody: {bodyJson}");
        Console.WriteLine("Exception:");
        Console.WriteLine($"\tError message: {exception.Message}");
        Console.WriteLine($"Stack trace: {exception.StackTrace}");

        LogExceptionResponse response = new LogExceptionResponse
        {
            Message = "Exceção registrada com sucesso.",
            Variant = AlertVariant.Success,
            SuccessfullyLogged = true,
        };

        return response;
    }
}