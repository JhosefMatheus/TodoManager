using System.ComponentModel.DataAnnotations;

namespace Api.Models.Queries.Project;

public class CheckProjectNameChangedQuery : BaseQuery
{
    [Required(ErrorMessage = "O campo \"id\" é obrigatório.")]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }
}