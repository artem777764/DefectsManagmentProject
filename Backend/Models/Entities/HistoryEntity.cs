namespace Backend.Models.Entities;

public class HistoryEntity
{
    public int Id { get; set; }
    public int DefectId { get; set; }
    public int UserId { get; set; }
    public int DefectStatusId { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserEntity User { get; set; } = null!;
    public DefectEntity Defect { get; set; } = null!;
    public DefectStatusEntity DefectStatus { get; set; } = null!;
}