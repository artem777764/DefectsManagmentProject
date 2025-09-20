namespace Backend.Models.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public int DefectId { get; set; }
    public int UserId { get; set; }
    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserEntity User { get; set; } = null!;
    public DefectEntity Defect { get; set; } = null!;
}