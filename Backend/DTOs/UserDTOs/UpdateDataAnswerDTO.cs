namespace Backend.DTOs.UserDTOs;

public record UpdateDataAnswerDTO
{
    public bool Successful { get; set; }
    public int? UserId { get; set; }
    public string? Message { get; set; }
}