namespace Stargate.Application.Queries.GetPerson;

public class GetPersonHandler : IRequestHandler<GetPersonQuery, GetPersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetPersonResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person is null)
            throw new NotFoundException($"No person found with ID {request.Id}");

        var dto = _mapper.Map<PersonAstronautDto>(person);

        return new GetPersonResponse
        {
            Data = dto,
            Message = "Person retrieved successfully",
            ResponseCode = (int)HttpStatusCode.OK,
            Success = true
        };
    }
}