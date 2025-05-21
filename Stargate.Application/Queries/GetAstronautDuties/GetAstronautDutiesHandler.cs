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

        var sortedDuties = duties
            .OrderByDescending(d => d.DutyStartDate)
            .ToList();

        var dtoList = _mapper.Map<List<AstronautDutyDto>>(sortedDuties);

        return new GetAstronautDutiesResponse
        {
            Data = dtoList,
            Success = true,
            Message = duties.Any()
                ? "Astronaut duties retrieved successfully"
                : "No astronaut duties found",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}