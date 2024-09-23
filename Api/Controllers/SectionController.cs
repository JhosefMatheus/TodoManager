using Api.Models.DTO.Section;
using Api.Models.Responses.Section;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("section")]
public class SectionController : ControllerBase
{
    private readonly SectionService sectionService;

    public SectionController(SectionService sectionService)
    {
        this.sectionService = sectionService;
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateSectionDTO createSectionDTO)
    {
        CreateSectionResponse createSectionResponse = this.sectionService.CreateSection(createSectionDTO);

        object response = createSectionResponse.ToJson();

        return Ok(response);
    }
}