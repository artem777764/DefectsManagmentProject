using Backend.DTOs.DefectDTOs;
using Backend.Models.Entities;

namespace Backend.Extensions;

public static class DefectExtensions
{
    public static DefectEntity ToEntity(this CreateDefectDTO createDefectDTO, int creatorId)
    {
        return new DefectEntity
        {
            Title = createDefectDTO.Title,
            Description = createDefectDTO.Description,
            ProjectId = createDefectDTO.ProjectId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            PriorityId = createDefectDTO.PriorityId,
            Deadline = createDefectDTO.Deadline,
            CreatorId = creatorId,
        };
    }

    public static DefectEntity Update(this DefectEntity defectEntity, UpdateDefectDTO updateDefectDTO)
    {
        defectEntity.Title = updateDefectDTO.Title;
        defectEntity.Description = updateDefectDTO.Description;
        defectEntity.UpdatedAt = DateTime.UtcNow;
        defectEntity.PriorityId = updateDefectDTO.PriorityId;
        defectEntity.Deadline = updateDefectDTO.Deadline;

        return defectEntity;
    }

    public static DefectEntity UpdateExecutor(this DefectEntity defectEntity, AppointmentDTO appointmentDTO)
    {
        defectEntity.ExecutorId = appointmentDTO.ExecutorId;
        
        return defectEntity;
    }

    public static GetDefectDTO ToDTO(this DefectEntity defectEntity)
    {
        return new GetDefectDTO
        {
            Id = defectEntity.Id,
            Title = defectEntity.Title,
            Description = defectEntity.Description,
            StatusName = defectEntity.DefectStatus.Name,
            CreatedAt = defectEntity.CreatedAt,
            UpdatedAt = defectEntity.UpdatedAt,
            PriorityName = defectEntity.Priority.Name,
            Deadline = defectEntity.Deadline,
            CreatorId = defectEntity.CreatorId,
            ExecutorId = defectEntity.ExecutorId,
        };
    }
}