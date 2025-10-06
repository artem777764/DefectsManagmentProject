using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IPriorityRepository
{
    Task<List<PriorityEntity>> GetPrioritiesAsync();
}
