using Api.Models.DTO.ProjectSection;
using Api.Models.Responses.ProjectSection;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("section")]
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
}