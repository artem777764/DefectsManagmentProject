namespace Backend.DTOs.UserDTOs;

public record AuthorizeDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}