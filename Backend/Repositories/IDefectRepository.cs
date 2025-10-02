using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IDefectRepository
{
    Task<int> CreateDefectAsync(DefectEntity defect);
    Task<DefectEntity?> GetByIdAsync(int defectId);
    Task<List<DefectEntity>> GetDefectsAsync();
    Task RemoveByIdAsync(int defectId);
    Task<int?> UpdateDefectAsync(DefectEntity defect);
}
