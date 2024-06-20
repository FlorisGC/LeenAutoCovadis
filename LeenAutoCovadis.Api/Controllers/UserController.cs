using LeenAutoCovadis.Api.Models;
using LeenAutoCovadis.Api.Services;
using LeenAutoCovadis.shared.Enums;
using LeenAutoCovadis.shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeenAutoCovadis.Api.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService userService = userService;

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = userService.GetUsers();
        return Ok(users);
    }

    // Door het toevoegen van de fallback policy mag je hier niet bij als je niet ingelogd bent
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = userService.GetUserById(id);
        return Ok(user);
    }

    [AllowAnonymous]
   // [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        var createdUser = userService.CreateUser(user);

        return Ok(createdUser);
    }

    [AllowAnonymous]
    //[Authorize(Roles = nameof(UserRole.User))]
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    //[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.User))]
    [HttpPatch("assign-role")]
    public IActionResult AssignRole([FromBody] AssignRoleRequest request)
    {
        var result = userService.AssignRole(request);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest registerRequest)
    {
        var user = new User
        {
            Name = registerRequest.Name,
            Email = registerRequest.Email,
            Password = registerRequest.Password // Ensure you handle password hashing
        };

        var createdUser = userService.CreateUser(user);
        return Ok(createdUser);
    }

}