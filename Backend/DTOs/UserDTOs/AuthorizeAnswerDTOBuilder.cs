namespace Backend.DTOs.UserDTOs;

public record AuthorizeAnswerDTOBuilder
{
    private readonly AuthorizeAnswerDTO _authorizeResult;

    public AuthorizeAnswerDTOBuilder()
    {
        _authorizeResult = new AuthorizeAnswerDTO();
    }

    public AuthorizeAnswerDTOBuilder SetUserId(int userId)
    {
        _authorizeResult.UserId = userId;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetRoleId(int roleId)
    {
        _authorizeResult.RoleId = roleId;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetJwtToken(string jwtToken)
    {
        _authorizeResult.JwtToken = jwtToken;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetJwtCookieName(string jwtCookieToken)
    {
        _authorizeResult.JwtCookieName = jwtCookieToken;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetExpireHours(int expireHours)
    {
        _authorizeResult.ExpireHours = expireHours;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetMessage(string message)
    {
        _authorizeResult.Message = message;
        return this;
    }

    public AuthorizeAnswerDTOBuilder SetSuccessful()
    {
        _authorizeResult.Successful = true;
        return this;
    }

    public AuthorizeAnswerDTO Build()
    {
        return _authorizeResult;
    }
}