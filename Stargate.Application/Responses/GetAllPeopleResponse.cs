namespace Stargate.Application.Responses;

public class GetAllPeopleResponse : BaseResponse
{
    public List<PersonAstronautDto> Data { get; set; } = new();
}