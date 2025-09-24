using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;

namespace Backend.Services;

public interface IUserService
{
    Task<RegisterAnswerDTO> RegisterUserAsync(CreateUserDTO createUserDto);
    Task<UpdateDataAnswerDTO> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto);
    Task<AuthorizeAnswerDTO> AuthorizeAsync(AuthorizeDTO authorizeDTO);
    Task<List<GetUserDTO>> GetUsersAsync();
    Task<GetUserDTO?> GetUserByIdAsync(int userId);
    Task RemoveByIdAsync(int userId);
}