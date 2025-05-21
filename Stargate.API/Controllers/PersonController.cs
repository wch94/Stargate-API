using Stargate.Application.Queries.GetAstronautDuties;
using Stargate.Application.Responses;

namespace Stargate.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetPeopleResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] string? name,
        [FromQuery] string? sortBy = "name",
        [FromQuery] bool desc = false,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var response = await _mediator.Send(new GetPeopleQuery(name, sortBy, desc, page, pageSize));
        return StatusCode(response.ResponseCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _mediator.Send(new GetPersonQuery(id));
        return StatusCode(response.ResponseCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.ResponseCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        var response = await _mediator.Send(new UpdatePersonEnvelope(id, command));
        return StatusCode(response.ResponseCode, response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeletePersonCommand(id));
        return StatusCode(response.ResponseCode, response);
    }

}