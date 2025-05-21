namespace Stargate.Application.Commands.AstronautDuty.Create;

public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDutyCommand, CreateAstronautDutyResponse>
{
    private readonly IAstronautDutyRepository _dutyRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAstronautDutyHandler> _logger;

    public CreateAstronautDutyHandler(
        IAstronautDutyRepository dutyRepo,
        IPersonRepository personRepo,
        IMapper mapper,
        ILogger<CreateAstronautDutyHandler> logger)
    {
        _dutyRepo = dutyRepo;
        _personRepo = personRepo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateAstronautDutyResponse> Handle(CreateAstronautDutyCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepo.GetByIdAsync(request.PersonId, cancellationToken);
        if (person is null)
            throw new NotFoundException($"Person with ID {request.PersonId} not found.");

        // Load astronaut detail if not already included
        var astronautDetail = person.AstronautDetail;
        if (astronautDetail == null)
        {
            _logger.LogWarning("Astronaut detail is missing for person {PersonId}", request.PersonId);
            throw new CustomValidationException("AstronautDetail", $"Person with ID {request.PersonId} does not have an astronaut detail record.");
        }

        // Check for current active duty
        var currentDuty = person.AstronautDuties
            .FirstOrDefault(d => d.DutyEndDate == null);

        if (currentDuty != null)
        {
            if (request.DutyStartDate <= currentDuty.DutyStartDate)
            {
                _logger.LogWarning("New duty start date must be after the current duty's start date.");
                throw new CustomValidationException("AstronautDuty", "New duty start date must be after the current duty's start date.");
            }

            currentDuty.DutyEndDate = request.DutyStartDate.AddDays(-1);
        }

        // Retirement logic → update AstronautDetail.CareerEndDate
        if (request.DutyTitle?.Trim().ToUpperInvariant() == "RETIRED")
        {
            astronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1);
        }

        _logger.LogInformation("Creating duty for person {PersonId}", request.PersonId);

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