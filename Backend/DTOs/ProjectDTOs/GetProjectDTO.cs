namespace Backend.DTOs.ProjectDTOs;

public record GetProjectDTO
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}