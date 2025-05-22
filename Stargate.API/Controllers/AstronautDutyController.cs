namespace Stargate.API.Controllers;

[ApiController]
[Route("v1/duty")]
public class AstronautDutyController : ControllerBase
{
    private readonly IMediator _mediator;

    public AstronautDutyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-person/{personId}")]
    public async Task<IActionResult> GetByPerson(int personId)
    {
        var response = await _mediator.Send(new GetAstronautDutiesQuery(personId));
        return StatusCode(response.ResponseCode, response);
    }

    [HttpPost("person/{personId}")]
    public async Task<IActionResult> CreateForPerson(int personId, [FromBody] CreateAstronautDutyRequest request)
    {
        var command = new CreateAstronautDutyCommand(
            personId,
            request.Rank,
            request.DutyTitle,
            request.DutyStartDate,
            request.DutyEndDate
        );

        var response = await _mediator.Send(command);
        return StatusCode(response.ResponseCode, response);
    }

}