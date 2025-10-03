namespace Backend.Models.Entities;

public class DefectEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int PriorityId { get; set; }
    public DateTime? Deadline { get; set; }
    public int CreatorId { get; set; }
    public int? ExecutorId { get; set; }

    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public List<HistoryEntity> History { get; set; } = new List<HistoryEntity>();
    public ProjectEntity Project { get; set; } = null!;
    public DefectStatusEntity DefectStatus { get; set; } = null!;
    public PriorityEntity Priority { get; set; } = null!;
    public List<AttachmentEntity> Attachments { get; set; } = new List<AttachmentEntity>();
    public UserEntity Creator { get; set; } = null!;
    public UserEntity? Executor { get; set; }
}