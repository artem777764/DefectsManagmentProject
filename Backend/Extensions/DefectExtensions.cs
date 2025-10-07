using System.Globalization;
using Backend.DTOs.DefectDTOs;
using Backend.Models.Entities;

namespace Backend.Extensions;

public static class DefectExtensions
{
    private static DateTime? ParseDate(string? dateString)
    {
        DateTime? parsedDeadline = null;

        if (!string.IsNullOrWhiteSpace(dateString) &&
            DateTimeOffset.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            parsedDeadline = result.UtcDateTime;
        }

        return parsedDeadline;
    }
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
            Deadline = ParseDate(createDefectDTO.Deadline),
            CreatorId = creatorId,
        };
    }

    public static DefectEntity Update(this DefectEntity defectEntity, UpdateDefectDTO updateDefectDTO)
    {
        defectEntity.Title = updateDefectDTO.Title;
        defectEntity.Description = updateDefectDTO.Description;
        defectEntity.UpdatedAt = DateTime.UtcNow;
        defectEntity.PriorityId = updateDefectDTO.PriorityId;
        defectEntity.Deadline = ParseDate(updateDefectDTO.Deadline);

        return defectEntity;
    }

    public static DefectEntity UpdateExecutor(this DefectEntity defectEntity, AppointmentDTO appointmentDTO)
    {
        defectEntity.ExecutorId = appointmentDTO.ExecutorId;
        
        return defectEntity;
    }

    public static GetDefectDTO ToDTO(this DefectWithLatestHistory defectWithLatestHistory)
    {
        return new GetDefectDTO
        {
            Id = defectWithLatestHistory.DefectId,
            Title = defectWithLatestHistory.DefectTitle,
            Description = defectWithLatestHistory.DefectDescription,
            StatusId = defectWithLatestHistory.StatusId,
            StatusName = defectWithLatestHistory.StatusName,
            CreatedAt = defectWithLatestHistory.CreatedAt,
            UpdatedAt = defectWithLatestHistory.UpdatedAt,
            PriorityId = defectWithLatestHistory.ProjectId,
            PriorityName = defectWithLatestHistory.PriorityName,
            Deadline = defectWithLatestHistory.Deadline,
            CreatorId = defectWithLatestHistory.CreatorId,
            ExecutorId = defectWithLatestHistory.ExecutorId,
        };
    }
}