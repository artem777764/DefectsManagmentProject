using Backend.DTOs.PriorityDTOs;
using Backend.Models.Entities;

namespace Backend.Extensions;

public static class PriorityExtensions
{
    public static GetPriorityDTO ToDTO(this PriorityEntity priorityEntity)
    {
        return new GetPriorityDTO
        {
            Value = priorityEntity.Value,
            Name = priorityEntity.Name,
        };
    }
}