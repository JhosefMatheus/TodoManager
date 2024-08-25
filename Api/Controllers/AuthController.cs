using Microsoft.AspNetCore.Mvc;
using Api.Models.DTO.Auth;

namespace Api.Controllers;

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