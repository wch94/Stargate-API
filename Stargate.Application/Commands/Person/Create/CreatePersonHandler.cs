namespace Stargate.Application.Commands.Person.Create;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public CreatePersonHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<CreatePersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Domain.Entities.Person
        {
            Name = request.Name
        };

        // Create astronaut detail if any relevant values are provided
        if (!string.IsNullOrWhiteSpace(request.CurrentRank) ||
            !string.IsNullOrWhiteSpace(request.CurrentDutyTitle) ||
            request.CareerStartDate.HasValue || request.CareerEndDate.HasValue)
        {
            person.AstronautDetail = new AstronautDetail
            {
                CurrentRank = request.CurrentRank ?? string.Empty,
                CurrentDutyTitle = request.CurrentDutyTitle ?? string.Empty,
                CareerStartDate = request.CareerStartDate ?? DateTime.UtcNow,
                CareerEndDate = request.CareerEndDate,
                Person = person
            };
        }

        await _personRepository.AddAsync(person, cancellationToken);

        var dto = _mapper.Map<PersonAstronautDto>(person);

        return new CreatePersonResponse
        {
            Data = dto,
            Message = "Person created successfully",
            ResponseCode = (int)HttpStatusCode.Created,
            Success = true
        };
    }
}