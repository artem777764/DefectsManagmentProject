namespace Backend.DTOs.UserDTOs;

public class RegisterAnswerDTOBuilder
{
    private readonly RegisterAnswerDTO _registerResult;

    public RegisterAnswerDTOBuilder()
    {
        _registerResult = new RegisterAnswerDTO();
        _registerResult.Successful = false;
    }
    
    public RegisterAnswerDTOBuilder SetMessage(string message)
    {
        _registerResult.Message = message;
        return this;
    }

    public RegisterAnswerDTOBuilder SetUserId(int userId)
    {
        _registerResult.UserId = userId;
        return this;
    }

    public RegisterAnswerDTOBuilder SetSuccessful()
    {
        _registerResult.Successful = true;
        return this;
    }

    public RegisterAnswerDTO Build()
    {
        return _registerResult;
    }
}