using Api.Models.DTO.ProjectSection;
using Api.Models.Queries.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("project-section")]
public class SectionController : ControllerBase
{
    private readonly ProjectSectionService projectSectionService;

    public SectionController(ProjectSectionService projectSectionService)
    {
        this.projectSectionService = projectSectionService;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateProjectSectionDTO createSectionDTO)
    {
        CreateProjectSectionResponse createSectionResponse = this.projectSectionService.CreateProjectSection(createSectionDTO);

        object response = createSectionResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("check-exists")]
    public ActionResult CheckProjectSectionExists([FromQuery] string query)
    {
        CheckProjectSectionExistsQuery checkProjectSectionExistsQuery = CheckProjectSectionExistsQuery
            .FromJson<CheckProjectSectionExistsQuery>(query);

        CheckProjectSectionExistsResponse checkProjectSectionExistsResponse = this.projectSectionService
            .CheckProjectSectionExists(checkProjectSectionExistsQuery);

        object response = checkProjectSectionExistsResponse.ToJson();

        return Ok(response);
    }

    [HttpGet("check-name-changed")]
    public ActionResult CheckProjectSectionNameChanged([FromQuery] string query)
    {
        CheckProjectSectionNameChangedQuery checkProjectSectionNameChangedQuery = CheckProjectSectionNameChangedQuery
            .FromJson<CheckProjectSectionNameChangedQuery>(query);

        CheckProjectSectionNameChangedResponse checkProjectSectionNameChangedResponse = this.projectSectionService
            .CheckProjectSectionNameChanged(checkProjectSectionNameChangedQuery);

        object response = checkProjectSectionNameChangedResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("archive/{id}")]
    public ActionResult Archive(int id)
    {
        ArchiveProjectSectionResponse archiveProjectSectionResponse = this.projectSectionService.Archive(id);

        object response = archiveProjectSectionResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("unarchive/{id}")]
    public ActionResult Unarchive(int id)
    {
        UnarchiveProjectSectionResponse unarchiveProjectSectionResponse = this.projectSectionService.Unarchive(id);

        object response = unarchiveProjectSectionResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("move-to/{id}")]
    public ActionResult MoveTo(int id, [FromBody] MoveProjectSectionToProjectDTO moveProjectSectionToProjectDTO)
    {
        MoveProjectSectionToProjectResponse moveProjectSectionToProjectResponse = this.projectSectionService
            .MoveTo(id, moveProjectSectionToProjectDTO);

        object response = moveProjectSectionToProjectResponse.ToJson();

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateProjectSectionById(int id, [FromBody] UpdateProjectSectionDTO updateProjectSectionDTO)
    {
        UpdateProjectSectionByIdResponse updateProjectSectionByIdResponse = this.projectSectionService
            .UpdateProjectSectionById(id, updateProjectSectionDTO);

        object response = updateProjectSectionByIdResponse.ToJson();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        DeleteProjectSectionResponse deleteProjectSectionResponse = this.projectSectionService.Delete(id);

        object response = deleteProjectSectionResponse.ToJson();

        return Ok(response);
    }
}