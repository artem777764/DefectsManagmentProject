using Backend.Models.Entities;

namespace Backend.Extensions;

public static class HistoryExtensions
{

    public static HistoryEntity ToHistoryCreatedEntity(this DefectEntity defectEntity)
    {
        return new HistoryEntity()
        {
            DefectId = defectEntity.Id,
            UserId = defectEntity.CreatorId,
            DefectStatusId = (int)HistoryStatuses.New,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static HistoryEntity ToHistoryAppointmentEntity(this DefectEntity defectEntity, int appointerId)
    {
        return new HistoryEntity()
        {
            DefectId = defectEntity.Id,
            UserId = appointerId,
            DefectStatusId = (int)HistoryStatuses.Work,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static HistoryEntity ToHistoryVerifyingEntity(this DefectEntity defectEntity, int senderId)
    {
        return new HistoryEntity()
        {
            DefectId = defectEntity.Id,
            UserId = senderId,
            DefectStatusId = (int)HistoryStatuses.Verifying,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static HistoryEntity ToHistoryDeniedEntity(this DefectEntity defectEntity, int userId)
    {
        return new HistoryEntity()
        {
            DefectId = defectEntity.Id,
            UserId = userId,
            DefectStatusId = (int)HistoryStatuses.Work,
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static HistoryEntity ToHistoryAcceptedEntity(this DefectEntity defectEntity, int userId)
    {
        return new HistoryEntity()
        {
            DefectId = defectEntity.Id,
            UserId = userId,
            DefectStatusId = (int)HistoryStatuses.Done,
            CreatedAt = DateTime.UtcNow,
        };
    }
}