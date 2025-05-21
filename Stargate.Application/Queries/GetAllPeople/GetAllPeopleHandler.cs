namespace Stargate.Application.Queries.GetAllPeople;

public class GetAllPeopleHandler : IRequestHandler<GetAllPeopleQuery, GetAllPeopleResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllPeopleHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetAllPeopleResponse> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var people = await _personRepository.GetAllAsync(cancellationToken);
        var dtoList = _mapper.Map<List<PersonAstronautDto>>(people);

        return new GetAllPeopleResponse
        {
            Data = dtoList,
            Message = "People retrieved successfully",
            ResponseCode = (int)HttpStatusCode.OK,
            Success = true
        };
    }
}