using System.Linq.Expressions;
using Backend.DTOs.DefectDTOs;
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

    public async Task<List<DefectWithLatestHistory>> GetDefectsAsync()
    {
        return await BuildDefectsQuery().ToListAsync();
    }

    public async Task<DefectWithLatestHistory?> GetByIdAsync(int defectId)
    {
        return await BuildDefectsQuery().FirstOrDefaultAsync(dh => dh.DefectId == defectId);
    }

    private IQueryable<DefectWithLatestHistory> BuildDefectsQuery()
    {
        return _context.Defects.Include(d => d.Priority)
                               .Include(d => d.Project)
                               .Include(d => d.Comments)
                               .Include(d => d.Attachments)
                               .Include(d => d.Creator)
                                   .ThenInclude(c => c.UserData)
                               .Include(d => d.Executor)
                                   .ThenInclude(e => e != null ? e.UserData : null)
                               .Select(d => new DefectWithLatestHistory
                               {
                                   DefectId = d.Id,
                                   DefectTitle = d.Title,
                                   DefectDescription = d.Description,
                                   StatusId = d.History.OrderByDescending(h => h.CreatedAt).FirstOrDefault()!.DefectStatus.Id,
                                   StatusName = d.History.OrderByDescending(h => h.CreatedAt).FirstOrDefault()!.DefectStatus.Name,
                                   CreatedAt = d.CreatedAt,
                                   UpdatedAt = d.UpdatedAt,
                                   PriorityName = d.Priority.Name,
                                   Deadline = d.Deadline,
                                   CreatorId = d.CreatorId,
                                   ExecutorId = d.ExecutorId,
                                   ProjectId = d.Project.Id,
                               })
                               .OrderByDescending(dh => dh.CreatedAt)
                               .AsNoTracking();
    }

    public async Task<DefectEntity?> GetByIdEntityAsync(int defectId)
    {
        return await _context.Defects.Include(d => d.Priority)
                                     .Include(d => d.Project)
                                     .Include(d => d.Comments)
                                     .Include(d => d.History)
                                         .ThenInclude(h => h.DefectStatus)
                                     .Include(d => d.Attachments)
                                     .Include(d => d.Creator)
                                         .ThenInclude(c => c.UserData)
                                     .Include(d => d.Executor)
                                         .ThenInclude(e => e != null ? e.UserData : null)
                                     .FirstOrDefaultAsync(d => d.Id == defectId);
    }

    public async Task<List<DefectWithLatestHistory>> GetByProjectAsync(int projectId, string? searchQuery, Expression<Func<DefectWithLatestHistory, bool>>? extraFilter = null)
    {
        IQueryable<DefectWithLatestHistory> query = BuildDefectsQuery().Where(dh => dh.ProjectId == projectId);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            string q = searchQuery.Trim();
            query = query.Where(dh => EF.Functions.ILike(dh.DefectTitle, $"%{q}%"));
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