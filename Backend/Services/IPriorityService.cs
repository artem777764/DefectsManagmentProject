using Backend.DTOs.PriorityDTOs;

namespace Backend.Services;

public interface IPriorityService
{
    Task<List<GetPriorityDTO>> GetPrioritiesAsync();
}
