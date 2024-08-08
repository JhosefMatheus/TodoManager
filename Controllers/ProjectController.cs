using Microsoft.AspNetCore.Mvc;

namespace TodoManager.Controllers;

[ApiController]
[Route("project")]
public class ProjectController : ControllerBase
{
    [HttpPost]
    public ActionResult Create()
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