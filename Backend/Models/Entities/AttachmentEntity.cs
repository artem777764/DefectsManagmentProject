namespace Backend.Models.Entities;

public class AttachmentEntity
{
    public int Id { get; set; }
    public int DefectId { get; set; }
    public required string FileId { get; set; }
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }
    public int UploadedById { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserEntity UploadedBy { get; set; } = null!;
    public DefectEntity Defect { get; set; } = null!;
}