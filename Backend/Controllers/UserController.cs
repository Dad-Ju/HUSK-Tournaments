using Application.Dtos;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(GetUserHandler getUserHandler) : ControllerBase
{
    private readonly CreateUserHandler _createUserHandler;
    private readonly GetUserHandler _getUserHandler;

    public UserController(GetUserHandler getUserHandler, CreateUserHandler createUserHandler) : this(getUserHandler)
    {
        _getUserHandler = getUserHandler;
        _createUserHandler = createUserHandler;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _getUserHandler.HandleAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserDto { Id = user.Id, DisplayName = user.DisplayName });
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.DisplayName) || string.IsNullOrWhiteSpace(request.AuthProviderId))
            return BadRequest("Both DisplayName and AuthProviderId are required.");

        await _createUserHandler.HandleAsync(request);
        return CreatedAtAction(nameof(GetUser), new { id = request.Id }, request);
    }
}