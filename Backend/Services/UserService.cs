using backend.Services;
using Backend.DTOs.UserDTOs;
using Backend.Extensions;
using Backend.Models.Entities;
using Backend.Repositories;

namespace Backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public UserService(IUserRepository userRepository, IEncryptionService encryptionService)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public async Task<int> RegisterUserAsync(CreateUserDTO createUserDto)
    {
        string password = _encryptionService.HashPassword(createUserDto.Password);
        UserEntity userEntity = createUserDto.ToEntity(password, 1);

        int createdUserId = await _userRepository.CreateUserAsync(userEntity);
        return createdUserId;
    }

    public async Task<int> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto)
    {
        UserDataEntity userDataEntity = createUserDataDto.ToEntity();

        int createdUserDataId = await _userRepository.UpdateUserDataAsync(userDataEntity);
        return createdUserDataId;
    }

    public async Task<List<GetUserDTO>> GetUsersAsync()
    {
        return (await _userRepository.GetUsersAsync()).Select(u => u.ToDTO()).ToList();
    }

    public async Task<GetUserDTO?> GetUserByIdAsync(int userId)
    {
        UserEntity? userEntity = await _userRepository.GetByIdAsync(userId);
        if (userEntity == null) return null;
        else return userEntity.ToDTO();
    }

    public async Task RemoveByIdAsync(int userId)
    {
        await _userRepository.RemoveByIdAsync(userId);
    }
}