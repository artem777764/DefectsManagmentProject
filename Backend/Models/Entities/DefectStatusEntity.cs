namespace Backend.Models.Entities;

public class DefectStatusEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public List<DefectEntity> Defects { get; set; } = new List<DefectEntity>();
    public List<HistoryEntity> History { get; set; } = new List<HistoryEntity>();
}