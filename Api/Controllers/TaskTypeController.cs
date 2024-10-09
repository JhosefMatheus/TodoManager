using Api.Models.Responses.TaskType;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("task-type")]
public class TaskTypeController : ControllerBase
{
    private readonly TaskTypeService taskTypeService;

    public TaskTypeController(TaskTypeService taskTypeService)
    {
        this.taskTypeService = taskTypeService;
    }

    public ActionResult GetAll()
    {
        GetAllTaskTypesResponse getAllTaskTypesResponse = taskTypeService.GetAll();

        object response = getAllTaskTypesResponse.ToJson();

        return Ok(response);
    }
}