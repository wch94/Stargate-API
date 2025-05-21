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
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllPeopleQuery());
        return StatusCode(response.ResponseCode, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetPersonByIdQuery(id));
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
        var response = await _mediator.Send(command);
        return StatusCode(response.ResponseCode, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command)
    {
        var response = await _mediator.Send(new UpdatePersonEnvelope(id, command));
        return StatusCode(response.ResponseCode, response);
    }
}