namespace Backend.Models.Entities;

public class PriorityEntity
{
    public int Id { get; set; }
    public int Value { get; set; }
    public required string Name { get; set; }

    public List<DefectEntity> Defects { get; set; } = new List<DefectEntity>();
}