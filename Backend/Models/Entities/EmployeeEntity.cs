namespace Backend.Models.Entities;

public class EmployeeEntity
{
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }

    public UserEntity Employee { get; set; } = null!;
    public ProjectEntity Project { get; set; } = null!;
}