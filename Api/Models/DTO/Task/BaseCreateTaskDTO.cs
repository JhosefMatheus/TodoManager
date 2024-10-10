using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Task;

public abstract class BaseCreateTaskDTO
{
    [Required(ErrorMessage = "O campo \"taskTypeId\" é obrigatório.")]
    public int TaskTypeId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }
}