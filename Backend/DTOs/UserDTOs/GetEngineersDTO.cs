namespace Backend.DTOs.UserDTOs;

public record GetEngineerDTO
{
    public required int Value { get; set; }
    public required string Name { get; set; }
}