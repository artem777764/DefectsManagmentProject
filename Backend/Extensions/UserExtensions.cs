using Backend.DTOs.UserDTOs;
using Backend.Models.Entities;

namespace Backend.Extensions;

public static class UserExtensions
{
    public static UserEntity ToEntity(this CreateUserDTO userDto, string passwordHash, int roleId)
    {
        return new UserEntity
        {
            Email = userDto.Email,
            Login = userDto.Login,
            PasswordHash = passwordHash,
            RoleId = roleId,
        };
    }

    public static UserDataEntity ToEntity(this CreateUserDataDTO userDataDto, int userId)
    {
        return new UserDataEntity
        {
            Id = userId,
            Surname = userDataDto.Surname,
            Name = userDataDto.Name,
            Patronymic = userDataDto.Patronymic,
        };
    }

    public static GetUserDTO ToDTO(this UserEntity userEntity)
    {
        return new GetUserDTO
        {
            Id = userEntity.Id,
            Surname = userEntity.UserData?.Surname ?? "Неизвестно",
            Name = userEntity.UserData?.Name ?? "Неизвестно",
            Patronymic = userEntity.UserData?.Patronymic ?? "Неизвестно",
        };
    }

    public static GetUserExtendedDTO ToExtendedDTO(this UserEntity userEntity)
    {
        return new GetUserExtendedDTO
        {
            Id = userEntity.Id,
            Surname = userEntity.UserData?.Surname ?? "Неизвестно",
            Name = userEntity.UserData?.Name ?? "Неизвестно",
            Patronymic = userEntity.UserData?.Patronymic ?? "Неизвестно",
            Email = userEntity.Email,
            Login = userEntity.Login,
            Role = userEntity.Role.Name,
        };
    }
}