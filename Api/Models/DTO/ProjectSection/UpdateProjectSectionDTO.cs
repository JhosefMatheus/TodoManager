using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.ProjectSection;

public class UpdateProjectSectionDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public required string Name { get; set; }
}