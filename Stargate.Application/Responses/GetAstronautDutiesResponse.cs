namespace Stargate.Application.Responses;

public class GetAstronautDutiesResponse : BaseResponse
{
    public List<AstronautDutyDto> Data { get; set; } = new();
}