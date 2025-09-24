using System.Data.SqlTypes;
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
    private readonly IValidationService _validationService;
    private readonly RegisterAnswerDTOBuilder _registerAnswerBuilder;
    private readonly UpdateDataAnswerDTOBuilder _updateDataAnswerBuilder;

    public UserService(IUserRepository userRepository, IEncryptionService encryptionService, IValidationService validationService)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _validationService = validationService;
        _registerAnswerBuilder = new RegisterAnswerDTOBuilder();
        _updateDataAnswerBuilder = new UpdateDataAnswerDTOBuilder();
    }

    public async Task<RegisterAnswerDTO> RegisterUserAsync(CreateUserDTO createUserDto)
    {
        if (!_validationService.IsValidEmail(createUserDto.Email))
        {
            _registerAnswerBuilder.SetMessage("Некорректная почта");
            return _registerAnswerBuilder.Build();
        }

        if (!_validationService.IsValidLogin(createUserDto.Login))
        {
            _registerAnswerBuilder.SetMessage("Некорректный логин");
            return _registerAnswerBuilder.Build();
        }

        if (!_validationService.IsValidPassword(createUserDto.Password))
        {
            _registerAnswerBuilder.SetMessage("Некорректный пароль");
            return _registerAnswerBuilder.Build();
        }

        if (!createUserDto.PoliceAccept)
        {
            _registerAnswerBuilder.SetMessage("Не приняты условия");
            return _registerAnswerBuilder.Build();
        }

        string password = _encryptionService.HashPassword(createUserDto.Password);
        UserEntity userEntity = createUserDto.ToEntity(password, 1);

        int createdUserId = await _userRepository.CreateUserAsync(userEntity);
        _registerAnswerBuilder.SetUserId(createdUserId);
        _registerAnswerBuilder.SetMessage("Успешная регистрация");
        _registerAnswerBuilder.SetSuccessful();

        return _registerAnswerBuilder.Build();
    }

    public async Task<UpdateDataAnswerDTO> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto)
    {
        if (!_validationService.IsValidSurname(createUserDataDto.Surname))
        {
            _updateDataAnswerBuilder.SetMessage("Некорректная фамилия");
            return _updateDataAnswerBuilder.Build();
        }

        if (!_validationService.IsValidName(createUserDataDto.Name))
        {
            _updateDataAnswerBuilder.SetMessage("Некорректное имя");
            return _updateDataAnswerBuilder.Build();
        }

        if (!_validationService.IsValidPatronymic(createUserDataDto.Patronymic))
        {
            _updateDataAnswerBuilder.SetMessage("Некорректное отчество");
            return _updateDataAnswerBuilder.Build();
        }

        UserDataEntity userDataEntity = createUserDataDto.ToEntity();

        int createdUserDataId = await _userRepository.UpdateUserDataAsync(userDataEntity);
        _updateDataAnswerBuilder.SetUserId(createdUserDataId);
        _updateDataAnswerBuilder.SetMessage("Успешное онбволение данных");
        _updateDataAnswerBuilder.SetSuccessful();
        
        return _updateDataAnswerBuilder.Build();
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