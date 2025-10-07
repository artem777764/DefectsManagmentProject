using Backend.Models.Entities;

namespace Backend.DTOs.DefectDTOs;

public record DefectWithLatestHistory
{
    public required int DefectId { get; set; }
    public required string DefectTitle { get; set; }
    public string? DefectDescription { get; set; }
    public required int StatusId { get; set; }
    public required string StatusName { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required int PriorityId { get; set; }
    public required string PriorityName { get; set; }
    public DateTime? Deadline { get; set; }
    public required int CreatorId { get; set; }
    public int? ExecutorId { get; set; }
    public required int ProjectId { get; set; }
}