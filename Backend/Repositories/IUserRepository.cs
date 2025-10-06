using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IUserRepository
{
    Task<List<UserEntity>> GetEngineersAsync();
    Task<int> CreateUserAsync(UserEntity user);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByIdAsync(int userId);
    Task<UserEntity?> GetByLoginAsync(string login);
    Task<List<UserEntity>> GetUsersAsync();
    Task<bool> IsEmailExist(string email);
    Task<bool> IsLoginExist(string login);
    Task RemoveByIdAsync(int userId);
    Task<int> UpdateUserDataAsync(UserDataEntity userData);
}
