namespace Backend.DTOs.DefectDTOs;

public record GetDefectDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required int StatusId { get; set; }
    public required string StatusName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required string PriorityName { get; set; }
    public DateTime? Deadline { get; set; }
    public int CreatorId { get; set; }
    public int? ExecutorId { get; set; }
}