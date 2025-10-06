using System.Linq.Expressions;
using Backend.DTOs.DefectDTOs;
using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IDefectRepository
{
    Task<DefectEntity?> GetByIdEntityAsync(int defectId);
    Task<int> CreateDefectAsync(DefectEntity defect);
    Task<DefectWithLatestHistory?> GetByIdAsync(int defectId);
    Task<List<DefectWithLatestHistory>> GetByProjectAsync(int projectId, string? searchQuery, Expression<Func<DefectWithLatestHistory, bool>>? extraFilter = null);
    Task<List<DefectWithLatestHistory>> GetDefectsAsync();
    Task RemoveByIdAsync(int defectId);
    Task<int?> UpdateDefectAsync(DefectEntity defect);
}
