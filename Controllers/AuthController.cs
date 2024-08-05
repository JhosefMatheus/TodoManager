using Microsoft.AspNetCore.Mvc;

namespace TodoManager.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    [HttpPost("sign-in")]
    public ActionResult SignIn()
    {
        return Ok();
    }
}