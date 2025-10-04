using Backend.DTOs;
using Backend.DTOs.DefectDTOs;

namespace Backend.Services;

public interface IDefectService
{
    Task<List<GetDefectDTO>> GetByProjectAsync(int projectId, int userId, int roleId, string? searchQuery);
    Task<IdDTO> Accept(int defectId, int userId);
    Task<IdDTO> AppointmentExecutorAsync(AppointmentDTO appointmentDTO, int appointerId);
    Task<IdDTO> CreateDefectAsync(CreateDefectDTO createDefectDTO, int creatorId);
    Task<IdDTO> Deny(int defectId, int userId);
    Task<GetDefectDTO?> GetDefectByIdAsync(int defectId);
    Task<List<GetDefectDTO>> GetDefectsAsync();
    Task RemoveByIdAsync(int defectId);
    Task<IdDTO> SendOnVerifying(int defectId, int senderId);
    Task<IdDTO> UpdateDefectAsync(UpdateDefectDTO updateDefectDTO);
}
