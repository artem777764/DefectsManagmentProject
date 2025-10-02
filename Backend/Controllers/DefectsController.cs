using System.Security.Claims;
using Backend.DTOs;
using Backend.DTOs.DefectDTOs;
using Backend.Services;
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveDefectAsync([FromRoute] int id)
    {
        await _defectService.RemoveByIdAsync(id);
        return Ok();
    }
}