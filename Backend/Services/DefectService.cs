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
        DefectEntity defectEntity = (await _defectRepository.GetByIdAsync(updateDefectDTO.Id))!;
        await _defectRepository.UpdateDefectAsync(defectEntity.Update(updateDefectDTO));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> AppointmentExecutorAsync(AppointmentDTO appointmentDTO, int appointerId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdAsync(appointmentDTO.DefectId))!;
        await _defectRepository.UpdateDefectAsync(defectEntity.UpdateExecutor(appointmentDTO));

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryAppointmentEntity(appointerId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> SendOnVerifying(int defectId, int senderId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryVerifyingEntity(senderId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> Deny(int defectId, int userId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryDeniedEntity(userId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<IdDTO> Accept(int defectId, int userId)
    {
        DefectEntity defectEntity = (await _defectRepository.GetByIdAsync(defectId))!;

        await _historyRepository.CreateHistoryAsync(defectEntity.ToHistoryAcceptedEntity(userId));
        return new IdDTO { Id = defectEntity.Id };
    }

    public async Task<List<GetDefectDTO>> GetDefectsAsync()
    {
        return (await _defectRepository.GetDefectsAsync()).Select(d => d.ToDTO()).ToList();
    }

    public async Task<GetDefectDTO?> GetDefectByIdAsync(int defectId)
    {
        DefectEntity? defectEntity = await _defectRepository.GetByIdAsync(defectId);
        if (defectEntity == null) return null;
        else return defectEntity.ToDTO();
    }

    public async Task RemoveByIdAsync(int defectId)
    {
        await _defectRepository.RemoveByIdAsync(defectId);
    }
}