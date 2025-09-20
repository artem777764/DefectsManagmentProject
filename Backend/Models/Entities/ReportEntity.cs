namespace Backend.Models.Entities;

public class ReportEntity
{
    public int ProjectId { get; set; }
    public required string FileId { get; set; }

    public ProjectEntity Project { get; set; } = null!;
}