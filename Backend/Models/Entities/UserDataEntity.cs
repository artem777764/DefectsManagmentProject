namespace Backend.Models.Entities;

public class UserDataEntity
{
    public int Id { get; set; }
    public required string Surname { get; set; }
    public required string Name { get; set; }
    public required string Patronymic { get; set; }

    public UserEntity User { get; set; } = null!;
}