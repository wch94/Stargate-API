namespace Stargate.Application.Queries.GetAllPeople;

public class GetAllPeopleHandler : IRequestHandler<GetAllPeopleQuery, BaseResponse<List<PersonDto>>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetAllPeopleHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<PersonDto>>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var people = await _personRepository.GetAllAsync(cancellationToken);

        var dtoList = _mapper.Map<List<PersonDto>>(people);

        return new BaseResponse<List<PersonDto>>(dtoList)
        {
            Message = "People retrieved successfully",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}