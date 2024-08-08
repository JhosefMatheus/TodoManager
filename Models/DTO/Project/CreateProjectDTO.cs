using System.ComponentModel.DataAnnotations;

namespace TodoManager.Models.DTO.Project;

public class CreateProjectDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }
}