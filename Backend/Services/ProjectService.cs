using System.Threading.Tasks;
using Backend.DTOs.ProjectDTOs;
using Backend.Extensions;
using Backend.Repositories;

namespace Backend.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<GetProjectDTO>> GetProjectsForUserAsync(int userId, string? searchQuery)
    {
        return (await _projectRepository.GetProjectsForUserAsync(userId, searchQuery)).Select(p => p.ToDTO()).ToList();
    }
}