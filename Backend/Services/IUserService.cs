using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;

namespace Backend.Services;

public interface IUserService
{
    Task<List<GetEngineerDTO>> GetEngineersAsync();
    Task<RegisterAnswerDTO> RegisterUserAsync(CreateUserDTO createUserDto);
    Task<UpdateDataAnswerDTO> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto, int userId);
    Task<AuthorizeAnswerDTO> AuthorizeAsync(AuthorizeDTO authorizeDTO);
    Task<List<GetUserDTO>> GetUsersAsync();
    Task<GetUserExtendedDTO?> GetUserByIdAsync(int userId);
    Task RemoveByIdAsync(int userId);
}