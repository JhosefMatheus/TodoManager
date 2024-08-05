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
        Console.WriteLine(signInDTO.Login);
        Console.WriteLine(signInDTO.Password);

        return Ok();
    }
}