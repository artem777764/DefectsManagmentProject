using Backend.Models.Context;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class PriorityRepository : IPriorityRepository
{
    private readonly ApplicationDbContext _context;

    public PriorityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PriorityEntity>> GetPrioritiesAsync()
    {
        return await _context.Priorities.OrderBy(p => p.Value).ToListAsync();
    }
}