using Backend.DTOs;
using Backend.DTOs.DefectDTOs;

namespace Backend.Services;

public interface IDefectService
{
    Task<IdDTO> CreateDefectAsync(CreateDefectDTO createDefectDTO, int creatorId);
    Task<GetDefectDTO?> GetDefectByIdAsync(int defectId);
    Task<List<GetDefectDTO>> GetDefectsAsync();
    Task RemoveByIdAsync(int defectId);
    Task<IdDTO> UpdateDefectAsync(UpdateDefectDTO updateDefectDTO);
}
