namespace Backend.Models.Entities;

public class EmployeeEntity
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }

    public UserEntity User { get; set; } = null!;
    public ProjectEntity Project { get; set; } = null!;
}