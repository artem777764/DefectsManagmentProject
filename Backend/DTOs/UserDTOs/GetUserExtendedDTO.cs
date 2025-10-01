namespace Backend.DTOs.UserDTOs;

public record GetUserExtendedDTO
{
    public required int Id { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string Role { get; set; }
}