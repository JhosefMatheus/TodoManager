using Api.Models.DTO.Task;
using Api.Models.Responses.Task;

namespace Api.Models.Interfaces.Services;

public interface ITaskService
{
    public CreateTaskResponse Create(BaseCreateTaskDTO baseCreateTaskDTO);
}