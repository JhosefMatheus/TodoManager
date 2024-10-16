using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class ExceptionLogEntity : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public string Endpoint { get; set; }
    public string Method { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
    public DateTime CreatedAt { get; set; }
}