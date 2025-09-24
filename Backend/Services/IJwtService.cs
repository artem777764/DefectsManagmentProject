using Backend.Models.Entities;

namespace backend.Services;

public interface IJwtService
{
    string GenerateToken(UserEntity user);
    int GetExpireHours();
    string GetJwtCookieName();
}
