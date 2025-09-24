using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Backend.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expireHours;
    private readonly string _jwtCookieName;

    public JwtService(IOptions<JwtSettings> options)
    {
        _secretKey = options.Value.SecretKey;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expireHours = options.Value.ExpireHours;
        _jwtCookieName = options.Value.JwtCookieName;
    }

    public string GenerateToken(UserEntity user)
    {
        Claim[] claims = {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };

        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var Token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_expireHours)
        );
        var TokenValue = new JwtSecurityTokenHandler().WriteToken(Token);
        return TokenValue;
    }

    public int GetExpireHours()
    {
        return _expireHours;
    }

    public string GetJwtCookieName()
    {
        return _jwtCookieName;
    }
}