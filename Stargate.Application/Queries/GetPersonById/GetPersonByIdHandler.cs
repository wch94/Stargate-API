namespace Stargate.Application.Queries.GetPersonById;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByIdHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<GetPersonByIdResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person is null)
            throw new NotFoundException($"No person found with ID {request.Id}");

        var dto = _mapper.Map<PersonAstronautDto>(person);

        return new GetPersonByIdResponse
        {
            Data = dto,
            Message = "Person retrieved successfully",
            ResponseCode = (int)HttpStatusCode.OK,
            Success = true
        };
    }
}