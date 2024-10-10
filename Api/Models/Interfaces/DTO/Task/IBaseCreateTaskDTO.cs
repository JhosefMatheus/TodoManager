using Api.Models.DTO.Task;

namespace Api.Models.Interfaces.DTO.Task;

public interface IBaseCreateTaskDTO<T> where T : BaseCreateTaskDTO
{
    public static abstract T FromBaseCreateTaskDTO(BaseCreateTaskDTO baseCreateTaskDTO);
}