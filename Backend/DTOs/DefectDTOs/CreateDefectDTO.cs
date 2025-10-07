namespace Backend.DTOs.DefectDTOs;

public record CreateDefectDTO
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required int ProjectId { get; set; }
    public required int PriorityId { get; set; }
    public string? Deadline { get; set; }
}