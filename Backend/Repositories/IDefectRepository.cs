using System.Linq.Expressions;
using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IDefectRepository
{
    Task<int> CreateDefectAsync(DefectEntity defect);
    Task<DefectEntity?> GetByIdAsync(int defectId);
    Task<List<DefectEntity>> GetByProjectAsync(int projectId, Expression<Func<DefectEntity, bool>>? extraFilter = null);
    Task<List<DefectEntity>> GetDefectsAsync();
    Task RemoveByIdAsync(int defectId);
    Task<int?> UpdateDefectAsync(DefectEntity defect);
}
