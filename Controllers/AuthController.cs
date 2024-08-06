using Microsoft.AspNetCore.Mvc;
using TodoManager.Models.DTO.Auth;

namespace TodoManager.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    [HttpPost("sign-in")]
    public ActionResult SignIn([FromBody] SignInDTO signInDTO)
    {
        return Ok(signInDTO);
    }
}