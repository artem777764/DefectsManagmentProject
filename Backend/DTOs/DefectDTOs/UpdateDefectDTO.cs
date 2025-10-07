namespace Backend.DTOs.DefectDTOs;

public record UpdateDefectDTO
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required int PriorityId { get; set; }
    public string? Deadline { get; set; }
}