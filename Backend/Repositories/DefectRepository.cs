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
        return await _context.Defects.Include(d => d.DefectStatus)
                                     .Include(d => d.Priority)
                                     .Include(d => d.Project)
                                     .Include(d => d.Comments)
                                     .Include(d => d.History)
                                     .Include(d => d.Attachments)
                                     .Include(d => d.Creator)
                                     .ThenInclude(c => c.UserData)
                                     .Include(d => d.Executor)
                                     .ThenInclude(e => e != null ? e.UserData : null)
                                     .ToListAsync();
    }

    public async Task<DefectEntity?> GetByIdAsync(int defectId)
    {
        return await _context.Defects.Include(d => d.DefectStatus)
                                     .Include(d => d.Priority)
                                     .Include(d => d.Project)
                                     .Include(d => d.Comments)
                                     .Include(d => d.History)
                                     .Include(d => d.Attachments)
                                     .Include(d => d.Creator)
                                     .ThenInclude(c => c.UserData)
                                     .Include(d => d.Executor)
                                     .ThenInclude(e => e != null ? e.UserData : null)
                                     .FirstOrDefaultAsync(d => d.Id == defectId);
    }

    public async Task RemoveByIdAsync(int defectId)
    {
        DefectEntity? defectEntity = await _context.Defects.FirstOrDefaultAsync(d => d.Id == defectId);
        if (defectEntity != null) _context.Defects.Remove(defectEntity);
        await _context.SaveChangesAsync();
    }
}