namespace Backend.Models.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<EmployeeEntity> Employers { get; set; } = new List<EmployeeEntity>();
    public List<ReportEntity> Reports { get; set; } = new List<ReportEntity>();
    public List<DefectEntity> Defects { get; set; } = new List<DefectEntity>();
}