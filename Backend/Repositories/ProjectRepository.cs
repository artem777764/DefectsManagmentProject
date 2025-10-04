using Backend.Models.Context;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectEntity>> GetProjectsForUserAsync(int userId, string? searchQuery)
    {
        IQueryable<ProjectEntity> query = _context.Projects.AsQueryable();
        query = query.Where(p => p.Employers.Any(e => e.EmployeeId == userId));

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            string q = searchQuery.Trim();
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{q}%"));
        }

        return await query.ToListAsync();
    }
}