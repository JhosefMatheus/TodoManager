using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Task;

public class UpdateTaskDTO
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "O campo \"name\" é obrigatório e não pode ser uma string vazia.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo \"description\" é obrigatório e não pode ser uma string vazia")]
    public string? Description { get; set; }
}