namespace Stargate.Application.Responses;

public class UpdatePersonResponse : BaseResponse
{
    public PersonAstronautDto? Data { get; set; }
}