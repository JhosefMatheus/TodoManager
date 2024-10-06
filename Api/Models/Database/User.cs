using Api.Models.Interfaces.Database;

namespace Api.Models.Database;

public class User : IBaseIdentifierEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}