using Api.Models.DTO.Task;
using Api.Models.Responses.Task;
using Api.Services.Task;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers;

[ApiController]
[Route("task")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskService;

    public TaskController(TaskService taskService)
    {
        this.taskService = taskService;
    }

    [HttpPost]
    public ActionResult Create([FromBody] JObject createTaskDTO)
    {
        CreateTaskResponse createTaskResponse = taskService.Create(createTaskDTO);

        object response = createTaskResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("{id}/move-to")]
    public ActionResult MoveTo(int id, [FromBody] MoveTaskToDTO moveTaskToDTO)
    {
        MoveTaskToResponse moveTaskToResponse = taskService.MoveTo(id, moveTaskToDTO);

        object response = moveTaskToResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("{id}/archive")]
    public ActionResult Archive(int id)
    {
        ArchiveTaskResponse archiveTaskResponse = taskService.Archive(id);

        object response = archiveTaskResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("{id}/unarchive")]
    public ActionResult Unarchive(int id)
    {
        UnarchiveTaskResponse unarchiveTaskResponse = taskService.Unarchive(id);

        object response = unarchiveTaskResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateTaskDTO updateTaskDTO)
    {
        UpdateTaskResponse updateTaskResponse = taskService.Update(id, updateTaskDTO);

        object response = updateTaskResponse.ToJson();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        DeleteTaskResponse deleteTaskResponse = taskService.Delete(id);

        object response = deleteTaskResponse.ToJson();

        return Ok(response);
    }
}
