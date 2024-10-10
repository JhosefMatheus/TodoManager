using Api.Models.DTO.Task;
using Api.Models.Responses.Task;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult Create([FromBody] BaseCreateTaskDTO createTaskDTO)
    {
        CreateTaskResponse createTaskResponse = taskService.Create(createTaskDTO);

        object response = createTaskResponse.ToJson();

        return Ok();
    }
}