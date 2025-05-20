namespace Stargate.Application.Queries.GetPersonByName;

public class GetPersonByNameHandler : IRequestHandler<GetPersonByNameQuery, BaseResponse<PersonDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonByNameHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PersonDto>> Handle(GetPersonByNameQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByNameAsync(request.Name, cancellationToken);

        if (person is null)
            throw new NotFoundException($"No person found with name '{request.Name}'.");

        var dto = _mapper.Map<PersonDto>(person);

        return new BaseResponse<PersonDto>(dto)
        {
            Message = "Person found",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}
