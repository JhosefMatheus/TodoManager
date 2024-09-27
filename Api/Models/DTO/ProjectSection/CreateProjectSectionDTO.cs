using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.ProjectSection;

public class CreateProjectSectionDTO
{
    [Required(ErrorMessage = "O campo \"id\" é obrigatório.")]
    public int ProjectId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }
}