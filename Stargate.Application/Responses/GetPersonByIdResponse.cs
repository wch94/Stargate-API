namespace Stargate.Application.Responses;

public class GetPersonByIdResponse : BaseResponse
{
    public PersonAstronautDto? Data { get; set; }
}