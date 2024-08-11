using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TodoManager.Models.Exceptions.HttpExceptions;
using TodoManager.Models.Shared;

namespace TodoManager.Models.Queries.Project;

public class CheckProjectNameChangedQuery
{
    [Required(ErrorMessage = "O campo \"id\" é obrigatório.")]
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }

    public static CheckProjectNameChangedQuery FromJson(string base64Json)
    {
        byte[] data = Convert.FromBase64String(base64Json);
        string json = System.Text.Encoding.UTF8.GetString(data);

        CheckProjectNameChangedQuery query = JsonSerializer.Deserialize<CheckProjectNameChangedQuery>(json)
        ?? throw new BadHttpException(
            "Erro inesperado no sistema ao converter o json.",
            AlertVariant.Error
        );

        return query;
    }
}