using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Task;

public abstract class BaseCreateTaskDTO
{
    [Required(ErrorMessage = "O campo \"projectId\" é obrigatório.")]
    public int? ProjectId { get; set; }

    [Required(ErrorMessage = "O campo \"projectSectionId\" é obrigatório.")]
    public int? ProjectSectionId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public required string Name { get; set; }

    public string? Description { get; set; }
}