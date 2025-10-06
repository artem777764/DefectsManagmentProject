using Backend.DTOs.PriorityDTOs;
using Backend.Extensions;
using Backend.Repositories;

namespace Backend.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository _priorityRepository;

    public PriorityService(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public async Task<List<GetPriorityDTO>> GetPrioritiesAsync()
    {
        return (await _priorityRepository.GetPrioritiesAsync()).Select(p => p.ToDTO()).ToList();
    }
}