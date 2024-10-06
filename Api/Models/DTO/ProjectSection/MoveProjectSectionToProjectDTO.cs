using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.ProjectSection;

public class MoveProjectSectionToProjectDTO
{
    [Required(ErrorMessage = "O campo \"projectId\" é obrigatório.")]
    public int ProjectId { get; set; }
}