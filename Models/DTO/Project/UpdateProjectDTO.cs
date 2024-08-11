using System.ComponentModel.DataAnnotations;

namespace TodoManager.Models.DTO.Project;

public class UpdateProjectDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser um texto vazio.")]
    public string Name { get; set; }
}