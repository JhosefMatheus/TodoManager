using Microsoft.AspNetCore.Mvc;
using TodoManager.Models.DTO.Project;
using TodoManager.Models.Responses.Project;
using TodoManager.Services;

namespace TodoManager.Controllers;

[ApiController]
[Route("project")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateProjectDTO createProjectDTO)
    {
        CreateProjectResponse createProjectResponse = this.projectService.Create(createProjectDTO);

        object response = createProjectResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("check")]
    public ActionResult Check()
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return Ok();
    }
}