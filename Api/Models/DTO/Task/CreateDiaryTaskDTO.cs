using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Api.Models.Interfaces.DTO.Task;

namespace Api.Models.DTO.Task;

public class CreateDiaryTaskDTO : BaseCreateTaskDTO, IBaseCreateTaskDTO<CreateDiaryTaskDTO>
{
    [Required(ErrorMessage = "O campo \"days\" é obrigatório.")]
    public List<int> Days { get; set; }

    public static CreateDiaryTaskDTO FromBaseCreateTaskDTO(BaseCreateTaskDTO baseCreateTaskDTO)
    {
        PropertyInfo? daysPropertyInfo = baseCreateTaskDTO.GetType().GetProperty("days");

        bool hasDaysPropertyInfo = daysPropertyInfo != null;

        Console.WriteLine(hasDaysPropertyInfo);

        return new CreateDiaryTaskDTO()
        {
            Name = baseCreateTaskDTO.Name,
            TaskTypeId = baseCreateTaskDTO.TaskTypeId,
            Days = new List<int>(),
        };
    }
}