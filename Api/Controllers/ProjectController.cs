using Microsoft.AspNetCore.Mvc;
using Api.Models.DTO.Project;
using Api.Models.Queries.Project;
using Api.Models.Responses.Project;
using Api.Services;

namespace Api.Controllers;

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

    [HttpGet("check-exists")]
    public ActionResult CheckProjectExists([FromQuery] string name)
    {
        CheckProjectExistsResponse checkProjectExistsResponse = this.projectService.CheckProjectExists(name);

        object response = checkProjectExistsResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("check-name-changed")]
    public ActionResult CheckProjectNameChanged([FromQuery] string query)
    {
        CheckProjectNameChangedQuery checkProjectNameChangedQuery = CheckProjectNameChangedQuery.FromJson(query);

        CheckProjectNameChangedResponse checkProjectNameChangedResponse = this.projectService
            .CheckProjectNameChanged(checkProjectNameChangedQuery);

        object response = checkProjectNameChangedResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("get-all")]
    public ActionResult GetAllProjects()
    {
        GetAllProjectsResponse getAllProjectsResponse = this.projectService.GetAllProjects();

        object response = getAllProjectsResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult GetProjectById(int id)
    {
        GetProjectByIdResponse getProjectByIdResponse = this.projectService.GetProjectById(id);

        object response = getProjectByIdResponse.ToJson();

        return Ok(response);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateProjectDTO updateProjectDTO)
    {
        UpdateProjectResponse updateProjectResponse = this.projectService.UpdateProject(id, updateProjectDTO);

        object response = updateProjectResponse.ToJson();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        DeleteProjectResponse deleteProjectResponse = this.projectService.DeleteProject(id);

        object response = deleteProjectResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("archive/{id}")]
    public ActionResult Archive(int id)
    {
        ArchiveProjectResponse archiveProjectResponse = this.projectService.ArchiveProject(id);

        object response = archiveProjectResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("unarchive/{id}")]
    public ActionResult Unarchive(int id)
    {
        UnarchiveProjectResponse unarchiveProjectResponse = this.projectService.UnarchiveProject(id);

        object response = unarchiveProjectResponse.ToJson();

        return Ok(response);
    }
}