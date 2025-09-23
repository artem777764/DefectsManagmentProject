namespace Backend.DTOs.UserDTOs;

public record CreateUserDataDTO
{
    public required int UserId { get; set; }
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }
}