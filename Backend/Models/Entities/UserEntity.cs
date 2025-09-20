namespace Backend.Models.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required string PasswordHash { get; set; }
    public int RoleId { get; set; }

    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public List<HistoryEntity> History { get; set; } = new List<HistoryEntity>();
    public List<DefectEntity> DefectsCreatedBy { get; set; } = new List<DefectEntity>();
    public List<DefectEntity> DefectsExecutedBy { get; set; } = new List<DefectEntity>();
    public List<AttachmentEntity> Attachments { get; set; } = new List<AttachmentEntity>();
    public UserDataEntity UserData { get; set; } = null!;
    public List<EmployeeEntity> Employers { get; set; } = new List<EmployeeEntity>();
    public RoleEntity Role { get; set; } = null!;
}