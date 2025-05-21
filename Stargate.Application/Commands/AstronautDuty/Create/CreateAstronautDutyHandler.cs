namespace Stargate.Application.Commands.AstronautDuty.Create;

public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDutyCommand, CreateAstronautDutyResponse>
{
    private readonly IAstronautDutyRepository _dutyRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IMapper _mapper;

    public CreateAstronautDutyHandler(
        IAstronautDutyRepository dutyRepo,
        IPersonRepository personRepo,
        IMapper mapper)
    {
        _dutyRepo = dutyRepo;
        _personRepo = personRepo;
        _mapper = mapper;
    }

    public async Task<CreateAstronautDutyResponse> Handle(CreateAstronautDutyCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepo.GetByIdAsync(request.PersonId, cancellationToken);
        if (person is null)
            throw new NotFoundException($"Person with ID {request.PersonId} not found.");

        var duty = new Domain.Entities.AstronautDuty
        {
            PersonId = request.PersonId,
            Rank = request.Rank,
            DutyTitle = request.DutyTitle,
            DutyStartDate = request.DutyStartDate,
            DutyEndDate = request.DutyEndDate,
            Person = person
        };

        await _dutyRepo.AddAsync(duty, cancellationToken);

        return new CreateAstronautDutyResponse
        {
            Data = _mapper.Map<AstronautDutyDto>(duty),
            Message = "Astronaut duty created",
            ResponseCode = (int)HttpStatusCode.Created,
            Success = true
        };
    }
}