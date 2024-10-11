using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Task;

public class CreateDiaryTaskDTO : BaseCreateTaskDTO
{
    [Required(ErrorMessage = "O campo \"days\" é obrigatório.")]
    public List<int> Days { get; set; }
}