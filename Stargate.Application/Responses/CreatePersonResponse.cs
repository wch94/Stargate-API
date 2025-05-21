namespace Stargate.Application.Responses;

public class CreatePersonResponse : BaseResponse
{
    public PersonAstronautDto? Data { get; set; }
}