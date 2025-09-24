namespace Backend.DTOs.UserDTOs;

public record RegisterAnswerDTO
{
    public bool Successful { get; set; }
    public int? UserId { get; set; }
    public string? Message { get; set; }
}