using Backend.Models.Context;
using Backend.Models.Entities;

namespace Backend.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public HistoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateHistoryAsync(HistoryEntity history)
    {
        await _context.History.AddAsync(history);
        await _context.SaveChangesAsync();
        return history.Id;
    }
}