namespace Backend.DTOs.UserDTOs;

public record CreateUserDataDTO
{
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }
}