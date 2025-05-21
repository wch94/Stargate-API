namespace Stargate.Application.Responses;

public class GetPersonByNameResponse : BaseResponse
{
    public PersonAstronautDto? Data { get; set; }
}
