using System.Linq.Expressions;
using Backend.Models.Context;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class DefectRepository : IDefectRepository
{
    private readonly ApplicationDbContext _context;

    public DefectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateDefectAsync(DefectEntity defect)
    {
        await _context.Defects.AddAsync(defect);
        await _context.SaveChangesAsync();
        return defect.Id;
    }

    public async Task<int?> UpdateDefectAsync(DefectEntity defect)
    {
        DefectEntity? oldDefect = await _context.Defects.FirstOrDefaultAsync(d => d.Id == defect.Id);
        if (oldDefect == null) return null;
        else _context.Defects.Entry(oldDefect).CurrentValues.SetValues(defect);
        await _context.SaveChangesAsync();
        return defect.Id;
    }

    public async Task<List<DefectEntity>> GetDefectsAsync()
    {
        return await BuildDefectsQuery().ToListAsync();
    }

    public async Task<DefectEntity?> GetByIdAsync(int defectId)
    {
        return await BuildDefectsQuery().FirstOrDefaultAsync(d => d.Id == defectId);
    }

    private IQueryable<DefectEntity> BuildDefectsQuery()
    {
        return _context.Defects
                       .Include(d => d.DefectStatus)
                       .Include(d => d.Priority)
                       .Include(d => d.Project)
                       .Include(d => d.Comments)
                       .Include(d => d.History)
                       .Include(d => d.Attachments)
                       .Include(d => d.Creator)
                       .ThenInclude(c => c.UserData)
                       .Include(d => d.Executor)
                       .ThenInclude(e => e != null ? e.UserData : null)
                       .AsNoTracking();
    }

    public async Task<List<DefectEntity>> GetByProjectAsync(int projectId, string? searchQuery, Expression<Func<DefectEntity, bool>>? extraFilter = null)
    {
        IQueryable<DefectEntity> query = BuildDefectsQuery().Where(p => p.ProjectId == projectId);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            string q = searchQuery.Trim();
            query = query.Where(p => EF.Functions.ILike(p.Title, $"%{q}%"));
        }

        if (extraFilter != null) query = query.Where(extraFilter);
        return await query.ToListAsync();
    }

    public async Task RemoveByIdAsync(int defectId)
    {
        DefectEntity? defectEntity = await _context.Defects.FirstOrDefaultAsync(d => d.Id == defectId);
        if (defectEntity != null) _context.Defects.Remove(defectEntity);
        await _context.SaveChangesAsync();
    }
}