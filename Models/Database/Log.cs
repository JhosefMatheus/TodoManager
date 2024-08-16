namespace TodoManager.Models.Database;

public class Log
{
    public int Id { get; set; }
    public string Endpoint { get; set; } 
    public string Method { get; set; }
    public string Header { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
}