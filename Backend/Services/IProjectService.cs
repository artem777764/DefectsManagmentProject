using Backend.DTOs.ProjectDTOs;

namespace Backend.Services;

public interface IProjectService
{
    Task<List<GetProjectDTO>> GetProjectsForUserAsync(int userId, string? searchQuery);
}
