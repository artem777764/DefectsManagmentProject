using System.Security.Claims;
using Backend.DTOs;
using Backend.DTOs.DefectDTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("[controller]")]
[ApiController]
public class DefectsController : ControllerBase
{
    private readonly IDefectService _defectService;

    public DefectsController(IDefectService defectService)
    {
        _defectService = defectService;
    }

    [HttpPost("")]
    [AuthorizeByPolicy(Policy.Manager)]
    public async Task<IActionResult> CreateDefectAsync([FromBody] CreateDefectDTO createDefectDTO)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        IdDTO idDTO = await _defectService.CreateDefectAsync(createDefectDTO, userId);
        return Ok(idDTO);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateDefectDTO([FromBody] UpdateDefectDTO updateDefectDTO)
    {
        IdDTO idDTO = await _defectService.UpdateDefectAsync(updateDefectDTO);
        return Ok(idDTO);
    }

    [HttpPost("appointment")]
    [AuthorizeByPolicy(Policy.Manager)]
    public async Task<IActionResult> AppointDefectAsync([FromBody] AppointmentDTO appointmentDTO)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        IdDTO idDTO = await _defectService.AppointmentExecutorAsync(appointmentDTO, userId);
        return Ok(idDTO);
    }

    [HttpPost("verify/{id}")]
    [AuthorizeByPolicy(Policy.Engineer)]
    public async Task<IActionResult> VerifyDefectAsync([FromRoute] int id)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        IdDTO idDTO = await _defectService.SendOnVerifying(id, userId);
        return Ok(idDTO);
    }

    [HttpPost("deny/{id}")]
    [AuthorizeByPolicy(Policy.Manager)]
    public async Task<IActionResult> DenyDefectAsync([FromRoute] int id)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        IdDTO idDTO = await _defectService.Deny(id, userId);
        return Ok(idDTO);
    }

    [HttpPost("accept/{id}")]
    [AuthorizeByPolicy(Policy.Manager)]
    public async Task<IActionResult> AcceptDefectAsync([FromRoute] int id)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        IdDTO idDTO = await _defectService.Accept(id, userId);
        return Ok(idDTO);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetDefectsAsync()
    {
        return Ok(await _defectService.GetDefectsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDefectAsync([FromRoute] int id)
    {
        GetDefectDTO? getDefectDTO = await _defectService.GetDefectByIdAsync(id);
        if (getDefectDTO == null) return NotFound();
        else return Ok(getDefectDTO);
    }

    [HttpGet("project/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByProjectAsync([FromRoute] int id, [FromQuery] string? searchQuery)
    {
        Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);
        int roleId = int.Parse(User.FindFirst(ClaimTypes.Role)!.Value);

        return Ok(await _defectService.GetByProjectAsync(id, userId, roleId, searchQuery));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveDefectAsync([FromRoute] int id)
    {
        await _defectService.RemoveByIdAsync(id);
        return Ok();
    }


}