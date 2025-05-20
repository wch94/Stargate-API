namespace Stargate.Application.Commands.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, BaseResponse<PersonDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PersonDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person
        {
            Name = request.Name
        };

        await _personRepository.AddAsync(person, cancellationToken);

        var dto = _mapper.Map<PersonDto>(person);

        return new BaseResponse<PersonDto>(dto)
        {
            Message = "Person created successfully",
            ResponseCode = (int)HttpStatusCode.Created
        };
    }
}