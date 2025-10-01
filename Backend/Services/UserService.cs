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
    private readonly IJwtService _jwtService;
    private readonly RegisterAnswerDTOBuilder _registerAnswerBuilder;
    private readonly UpdateDataAnswerDTOBuilder _updateDataAnswerBuilder;
    private readonly AuthorizeAnswerDTOBuilder _authorizeAnswerBuilder;

    public UserService(
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        IValidationService validationService,
        IJwtService jwtService
    )
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _validationService = validationService;
        _jwtService = jwtService;
        _registerAnswerBuilder = new RegisterAnswerDTOBuilder();
        _updateDataAnswerBuilder = new UpdateDataAnswerDTOBuilder();
        _authorizeAnswerBuilder = new AuthorizeAnswerDTOBuilder();
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

        if (await _userRepository.IsEmailExist(createUserDto.Email))
        {
            _registerAnswerBuilder.SetMessage("Почта занята");
            return _registerAnswerBuilder.Build();
        }

        if (await _userRepository.IsLoginExist(createUserDto.Login))
        {
            _registerAnswerBuilder.SetMessage("Логин занят");
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

    public async Task<UpdateDataAnswerDTO> UpdateUserDataAsync(CreateUserDataDTO createUserDataDto, int userId)
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

        if (createUserDataDto.Patronymic != null && !_validationService.IsValidPatronymic(createUserDataDto.Patronymic))
        {
            _updateDataAnswerBuilder.SetMessage("Некорректное отчество");
            return _updateDataAnswerBuilder.Build();
        }

        UserDataEntity userDataEntity = createUserDataDto.ToEntity(userId);

        int createdUserDataId = await _userRepository.UpdateUserDataAsync(userDataEntity);
        _updateDataAnswerBuilder.SetUserId(createdUserDataId);
        _updateDataAnswerBuilder.SetMessage("Успешное обновление данных");
        _updateDataAnswerBuilder.SetSuccessful();
        
        return _updateDataAnswerBuilder.Build();
    }

    public async Task<AuthorizeAnswerDTO> AuthorizeAsync(AuthorizeDTO authorizeDTO)
    {
        UserEntity? userEntity = await _userRepository.GetByEmailAsync(authorizeDTO.UserName);
        if (userEntity == null) userEntity = await _userRepository.GetByLoginAsync(authorizeDTO.UserName);
        if (userEntity == null)
        {
            _authorizeAnswerBuilder.SetMessage("Почта/Логин не существует");
            return _authorizeAnswerBuilder.Build();
        }

        if (!_encryptionService.VerifyPassword(authorizeDTO.Password, userEntity!.PasswordHash))
        {
            _authorizeAnswerBuilder.SetMessage("Неверный пароль");
            return _authorizeAnswerBuilder.Build();
        }

        string jwtToken = _jwtService.GenerateToken(userEntity);
        string JwtCookieName = _jwtService.GetJwtCookieName();
        int expireHours = _jwtService.GetExpireHours();

        if (userEntity.UserData != null) _authorizeAnswerBuilder.SetHasData();

        _authorizeAnswerBuilder.SetUserId(userEntity.Id)
                               .SetRoleId(userEntity.Role.Id)
                               .SetJwtToken(jwtToken)
                               .SetJwtCookieName(JwtCookieName)
                               .SetExpireHours(expireHours)
                               .SetMessage("Успешная авторизация")
                               .SetSuccessful();

        return _authorizeAnswerBuilder.Build();
    }

    public async Task<List<GetUserDTO>> GetUsersAsync()
    {
        return (await _userRepository.GetUsersAsync()).Select(u => u.ToDTO()).ToList();
    }

    public async Task<GetUserExtendedDTO?> GetUserByIdAsync(int userId)
    {
        UserEntity? userEntity = await _userRepository.GetByIdAsync(userId);
        if (userEntity == null) return null;
        else return userEntity.ToExtendedDTO();
    }

    public async Task RemoveByIdAsync(int userId)
    {
        await _userRepository.RemoveByIdAsync(userId);
    }
}