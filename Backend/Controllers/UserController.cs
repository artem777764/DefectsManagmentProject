using System.Runtime.CompilerServices;
using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userSerivce;

    public UserController(IUserService userService)
    {
        _userSerivce = userService;
    }

    [HttpPost("/")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO userDto)
    {
        int createdUserId = await _userSerivce.RegisterUserAsync(userDto);
        return Ok(createdUserId);
    }

    [HttpPut("/")]
    public async Task<IActionResult> UpdateUserDataAsync([FromBody] CreateUserDataDTO userDataDto)
    {
        int createdUserDataId = await _userSerivce.UpdateUserDataAsync(userDataDto);
        return Ok(createdUserDataId);
    }

    [HttpGet("/")]
    public async Task<IActionResult> GetUsersAsync()
    {
        return Ok(await _userSerivce.GetUsersAsync());
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetUsersAsync([FromRoute] int id)
    {
        GetUserDTO? userDTO = await _userSerivce.GetUserByIdAsync(id);
        if (userDTO == null) return NotFound();
        else return Ok(userDTO);
    }

    [HttpDelete("/{id}")]
    public async Task<IActionResult> RemoveUserAsync([FromRoute] int id)
    {
        await _userSerivce.RemoveByIdAsync(id);
        return Ok();
    }
}