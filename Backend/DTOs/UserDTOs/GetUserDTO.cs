namespace Backend.DTOs.UserDTOs;

public record GetUserDTO
{
    public required int Id { get; set; }
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }
}