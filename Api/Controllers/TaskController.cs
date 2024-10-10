using Api.Models.DTO.Task;
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
        return Ok();
    }
}