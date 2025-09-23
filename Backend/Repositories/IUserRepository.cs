using Backend.Models.Entities;

namespace Backend.Repositories;

public interface IUserRepository
{
    Task<int> CreateUserAsync(UserEntity user);
    Task<int> UpdateUserDataAsync(UserDataEntity userData);
    Task<List<UserEntity>> GetUsersAsync();
    Task<UserEntity?> GetByIdAsync(int userId);
    Task RemoveByIdAsync(int userId);
}
