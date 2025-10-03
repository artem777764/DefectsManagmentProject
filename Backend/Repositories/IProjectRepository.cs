using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IProjectRepository
{
    Task<List<ProjectEntity>> GetProjectsForUserAsync(int userId, string? searchQuery);
}
