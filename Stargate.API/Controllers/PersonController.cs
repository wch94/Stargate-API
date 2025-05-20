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

    //[HttpGet]
    //public async Task<IActionResult> GetPeople()
    //{
    //    try
    //    {
    //        var result = await _mediator.Send(new GetPeople()
    //        {

    //        });

    //        return this.GetResponse(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        return this.GetResponse(new BaseResponse()
    //        {
    //            Message = ex.Message,
    //            Success = false,
    //            ResponseCode = (int)HttpStatusCode.InternalServerError
    //        });
    //    }
    //}

    //[HttpGet("{name}")]
    //public async Task<IActionResult> GetPersonByName(string name)
    //{
    //    try
    //    {
    //        var result = await _mediator.Send(new GetPersonByName()
    //        {
    //            Name = name
    //        });

    //        return this.GetResponse(result);
    //    }
    //    catch (Exception ex)
    //    {
    //        return this.GetResponse(new BaseResponse()
    //        {
    //            Message = ex.Message,
    //            Success = false,
    //            ResponseCode = (int)HttpStatusCode.InternalServerError
    //        });
    //    }
    //}

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Create), result);
    }
}