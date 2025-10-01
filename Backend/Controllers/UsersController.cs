using System.Runtime.CompilerServices;
using System.Security.Claims;
using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userSerivce;

    public UsersController(IUserService userService)
    {
        _userSerivce = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO userDto)
    {
        RegisterAnswerDTO registerAnswerDTO = await _userSerivce.RegisterUserAsync(userDto);
        if (!registerAnswerDTO.Successful) return Conflict(registerAnswerDTO);
        else return Ok(registerAnswerDTO);
    }

    [AuthorizeByPolicy(Policy.Admin)]
    [HttpPut("")]
    public async Task<IActionResult> UpdateUserDataAsync([FromBody] CreateUserDataDTO userDataDto)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        UpdateDataAnswerDTO updateDataAnswerDTO = await _userSerivce.UpdateUserDataAsync(userDataDto, userId);
        if (!updateDataAnswerDTO.Successful) return Conflict(updateDataAnswerDTO);
        else return Ok(updateDataAnswerDTO);
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthorizeUserAsync([FromBody] AuthorizeDTO authorizeDTO)
    {
        AuthorizeAnswerDTO authorizeAnswerDTO = await _userSerivce.AuthorizeAsync(authorizeDTO);
        if (!authorizeAnswerDTO.Successful) return NotFound(authorizeAnswerDTO);

        Response.Cookies.Append(authorizeAnswerDTO.JwtCookieName!, authorizeAnswerDTO.JwtToken!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(authorizeAnswerDTO.ExpireHours)
        });
        return Ok(authorizeAnswerDTO);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUsersAsync()
    {
        return Ok(await _userSerivce.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync([FromRoute] int id)
    {
        GetUserDTO? userDTO = await _userSerivce.GetUserByIdAsync(id);
        if (userDTO == null) return NotFound();
        else return Ok(userDTO);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUserAsync([FromRoute] int id)
    {
        await _userSerivce.RemoveByIdAsync(id);
        return Ok();
    }
}