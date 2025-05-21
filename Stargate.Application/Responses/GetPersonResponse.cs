namespace Stargate.Application.Responses;

public class GetPersonResponse : BaseResponse
{
    public PersonAstronautDto? Data { get; set; }
}