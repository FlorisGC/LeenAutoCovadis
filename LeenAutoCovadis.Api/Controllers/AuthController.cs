using LeenAutoCovadis.shared.Enums;
using LeenAutoCovadis.shared.Requests;
using LeenAutoCovadis.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeenAutoCovadis.Api.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController(AuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var response = authService.Login(request);

        if (response == null)
        {
            return Unauthorized();
        }

        return Ok(response);
    }


    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpGet("secret")]
    public IActionResult Secret()
    {
        return Ok("secret");
    }
}