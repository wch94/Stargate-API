namespace Stargate.Application.Queries.GetAstronautDuties;

public class GetAstronautDutiesHandler
    : IRequestHandler<GetAstronautDutiesQuery, GetAstronautDutiesResponse>
{
    private readonly IAstronautDutyRepository _repo;
    private readonly IMapper _mapper;

    public GetAstronautDutiesHandler(IAstronautDutyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GetAstronautDutiesResponse> Handle(
        GetAstronautDutiesQuery request,
        CancellationToken cancellationToken)
    {
        var duties = await _repo.GetByPersonIdAsync(request.PersonId, cancellationToken);

        if (!duties.Any())
            throw new NotFoundException($"No astronaut duties found for person with ID {request.PersonId}");

        var dtoList = _mapper.Map<List<AstronautDutyDto>>(duties);

        return new GetAstronautDutiesResponse
        {
            Data = dtoList,
            Success = true,
            Message = "Astronaut duties retrieved successfully",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}