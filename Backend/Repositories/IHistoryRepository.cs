using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IHistoryRepository
{
    Task<int> CreateHistoryAsync(HistoryEntity history);
}
