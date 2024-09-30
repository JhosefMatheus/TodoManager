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
}