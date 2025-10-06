using System.Linq.Expressions;
using Backend.DTOs;
using Backend.DTOs.DefectDTOs;
using Backend.Extensions;
using Backend.Models.Entities;
using Backend.Repositories;

namespace Backend.Services;

public class DefectService : IDefectService
{
    private readonly IDefectRepository _defectRepository;
    private IHistoryRepository _historyRepository;

    public DefectService(IDefectRepository defectRepository, IHistoryRepository historyRepository)
    {
        _defectRepository = defectRepository;
        _historyRepository = historyRepository;
    }

    public async Task<IdDTO> CreateDefectAsync(CreateDefectDTO createDefectDTO, int creatorId)
    {
        DefectEntity defectEntity = createDefectDTO.ToEntity(creatorId);
        int defectId = await _defectRepository.CreateDefectAsync(defectEntity);

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryCreatedEntity());
        return new IdDTO { Id = defectId };
    }

    public async Task<IdDTO> UpdateDefectAsync(UpdateDefectDTO updateDefectDTO)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdEntityAsync(updateDefectDTO.Id))!;
        await _defectRepository.UpdateDefectAsync(defectEntity.Update(updateDefectDTO));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> AppointmentExecutorAsync(AppointmentDTO appointmentDTO, int appointerId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdEntityAsync(appointmentDTO.DefectId))!;
        await _defectRepository.UpdateDefectAsync(defectEntity.UpdateExecutor(appointmentDTO));

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryAppointmentEntity(appointerId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> SendOnVerifying(int defectId, int senderId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdEntityAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryVerifyingEntity(senderId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> Deny(int defectId, int userId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdEntityAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryDeniedEntity(userId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> Accept(int defectId, int userId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdEntityAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryAcceptedEntity(userId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<List<GetDefectDTO>> GetDefectsAsync()
    {
        return (await _defectRepository.GetDefectsAsync()).Select(d => d.ToDTO()).ToList();
    }

    public async Task<GetDefectDTO?> GetDefectByIdAsync(int defectId)
    {
        DefectWithLatestHistory? defectWithLatestHistory = await _defectRepository.GetByIdAsync(defectId);
        if (defectWithLatestHistory == null) return null;
        else return defectWithLatestHistory.ToDTO();
    }

    public async Task<List<GetDefectDTO>> GetByProjectAsync(int projectId, int userId, int roleId, string? searchQuery)
    {
        if (Role.Engineer == (Role)roleId)
        {
            Expression<Func<DefectWithLatestHistory, bool>> filter = dh => dh.ExecutorId == userId;
            return (await _defectRepository.GetByProjectAsync(projectId, searchQuery, filter)).Select(dh => dh.ToDTO()).ToList();
        }
        else
        {
            return (await _defectRepository.GetByProjectAsync(projectId, searchQuery)).Select(dh => dh.ToDTO()).ToList();
        }
    }

    public async Task RemoveByIdAsync(int defectId)
    {
        await _defectRepository.RemoveByIdAsync(defectId);
    }
}