using Backend.DTOs.ProjectDTOs;
using Backend.Models.Entities;

namespace Backend.Extensions;

public static class ProjectExtensions
{
    public static GetProjectDTO ToDTO(this ProjectEntity projectEntity)
    {
        return new GetProjectDTO
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
        };
    }
}