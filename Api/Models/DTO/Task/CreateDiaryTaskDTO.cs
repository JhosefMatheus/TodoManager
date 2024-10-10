using Api.Models.Interfaces.DTO.Task;

namespace Api.Models.DTO.Task;

public class CreateDiaryTaskDTO : BaseCreateTaskDTO, IBaseCreateTaskDTO<CreateDiaryTaskDTO>
{
    public static CreateDiaryTaskDTO FromBaseCreateTaskDTO(BaseCreateTaskDTO baseCreateTaskDTO)
    {
        throw new NotImplementedException();
    }
}