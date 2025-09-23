using Backend.DTOs;
using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;

namespace Backend.Services;

public interface IUserService
{
    Task<int> RegisterUserAsync(CreateUserDTO createUserDto);
    Task<int> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto);
    Task<List<GetUserDTO>> GetUsersAsync();
    Task<GetUserDTO?> GetUserByIdAsync(int userId);
    Task RemoveByIdAsync(int userId);
}