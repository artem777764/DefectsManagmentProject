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
    private readonly JwtCookieService _jwtCookieService;

    public UsersController(IUserService userService, JwtCookieService jwtCookieService)
    {
        _userSerivce = userService;
        _jwtCookieService = jwtCookieService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO userDto)
    {
        RegisterAnswerDTO registerAnswerDTO = await _userSerivce.RegisterUserAsync(userDto);
        if (!registerAnswerDTO.Successful) return Conflict(registerAnswerDTO);
        else return Ok(registerAnswerDTO);
    }

    [Authorize]
    [HttpPut("data")]
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

        Response.Cookies.Append(
            authorizeAnswerDTO.JwtCookieName!,
            authorizeAnswerDTO.JwtToken!,
            _jwtCookieService.GetAuthCookieOptions());
            
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

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(_jwtCookieService.CookieName, _jwtCookieService.GetAuthCookieOptions(true));
        return Ok();
    }
}