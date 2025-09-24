namespace Backend.DTOs.UserDTOs;

public class UpdateDataAnswerDTOBuilder
{
    private readonly UpdateDataAnswerDTO _updateResult;

    public UpdateDataAnswerDTOBuilder()
    {
        _updateResult = new UpdateDataAnswerDTO();
        _updateResult.Successful = false;
    }
    
    public UpdateDataAnswerDTOBuilder SetMessage(string message)
    {
        _updateResult.Message = message;
        return this;
    }

    public UpdateDataAnswerDTOBuilder SetUserId(int userId)
    {
        _updateResult.UserId = userId;
        return this;
    }

    public UpdateDataAnswerDTOBuilder SetSuccessful()
    {
        _updateResult.Successful = true;
        return this;
    }

    public UpdateDataAnswerDTO Build()
    {
        return _updateResult;
    }
}