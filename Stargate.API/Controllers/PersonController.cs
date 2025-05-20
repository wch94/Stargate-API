using Stargate.Application.Commands.UpdatePersonByName;

namespace Stargate.API.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;
    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllPeopleQuery());
        return StatusCode(response.ResponseCode, response);
    }

    [HttpGet("by-name")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        var response = await _mediator.Send(new GetPersonByNameQuery(name));
        return StatusCode(response.ResponseCode, response);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByName), new { name = result.Data?.Name }, result);
    }

    [HttpPut("by-name")]
    public async Task<IActionResult> UpdateByName([FromBody] UpdatePersonByNameCommand command)
    {
        var response = await _mediator.Send(command);
        return StatusCode(response.ResponseCode, response);
    }
}