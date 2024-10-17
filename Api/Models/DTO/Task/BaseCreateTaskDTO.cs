using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Task;

public class BaseCreateTaskDTO
{
    public int? ProjectId { get; set; }

    public int? ProjectSectionId { get; set; }

    public int TaskTypeId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public required string Name { get; set; }

    public string? Description { get; set; }
}