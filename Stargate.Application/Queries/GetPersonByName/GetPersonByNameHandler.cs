namespace Stargate.Application.Queries.GetPersonByName;

public class GetPersonByNameHandler : IRequestHandler<GetPersonByNameQuery, GetPersonByNameResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByNameHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetPersonByNameResponse> Handle(GetPersonByNameQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByNameContainsAsync(request.Name, cancellationToken);

        if (person is null)
            throw new NotFoundException($"No person found matching name '{request.Name}'.");

        var dto = _mapper.Map<PersonAstronautDto>(person);

        return new GetPersonByNameResponse
        {
            Data = dto,
            Message = "Person found",
            ResponseCode = (int)HttpStatusCode.OK,
            Success = true
        };
    }
}