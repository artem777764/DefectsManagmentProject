namespace Backend.DTOs.PriorityDTOs;

public record GetPriorityDTO
{
    public required int Value { get; set; }
    public required string Name { get; set; }
}