using Api.Models.DTO.Task;
using Api.Models.Responses.Task;
using Api.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("task")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskService;
    private readonly DiaryTaskService diaryTaskService;

    public TaskController(TaskService taskService, DiaryTaskService diaryTaskService)
    {
        this.taskService = taskService;
        this.diaryTaskService = diaryTaskService;
    }

    [HttpPost("diary-task")]
    public ActionResult Create([FromBody] CreateDiaryTaskDTO createTaskDTO)
    {
        CreateTaskResponse createTaskResponse = diaryTaskService.Create(createTaskDTO);

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
}
