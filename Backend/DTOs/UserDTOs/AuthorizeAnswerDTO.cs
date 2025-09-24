namespace Backend.DTOs.UserDTOs;

public record AuthorizeAnswerDTO
{
    public bool Successful { get; set; }
    public int? UserId { get; set; }
    public int? RoleId { get; set; }
    public string? JwtToken { get; set; }
    public string? JwtCookieName { get; set; }
    public int ExpireHours { get; set; }
    public string? Message { get; set; }
}