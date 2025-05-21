namespace Stargate.Application.Commands.Person.Update;

public class UpdatePersonByIdHandler : IRequestHandler<UpdatePersonEnvelope, UpdatePersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonByIdHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<UpdatePersonResponse> Handle(UpdatePersonEnvelope request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person is null)
            throw new NotFoundException($"No person found with ID {request.Id}");

        person.Name = request.Command.Name;

        if (person.AstronautDetail is null &&
            (!string.IsNullOrWhiteSpace(request.Command.CurrentRank) ||
             !string.IsNullOrWhiteSpace(request.Command.CurrentDutyTitle) ||
             request.Command.CareerStartDate.HasValue ||
             request.Command.CareerEndDate.HasValue))
        {
            person.AstronautDetail = new AstronautDetail
            {
                Person = person,
                CurrentRank = request.Command.CurrentRank ?? string.Empty,
                CurrentDutyTitle = request.Command.CurrentDutyTitle ?? string.Empty,
                CareerStartDate = request.Command.CareerStartDate ?? DateTime.UtcNow,
                CareerEndDate = request.Command.CareerEndDate
            };
        }
        else if (person.AstronautDetail is not null)
        {
            person.AstronautDetail.CurrentRank = request.Command.CurrentRank ?? person.AstronautDetail.CurrentRank;
            person.AstronautDetail.CurrentDutyTitle = request.Command.CurrentDutyTitle ?? person.AstronautDetail.CurrentDutyTitle;
            person.AstronautDetail.CareerStartDate = request.Command.CareerStartDate ?? person.AstronautDetail.CareerStartDate;
            person.AstronautDetail.CareerEndDate = request.Command.CareerEndDate;
        }

        await _personRepository.UpdateAsync(person, cancellationToken);

        var dto = _mapper.Map<PersonAstronautDto>(person);

        return new UpdatePersonResponse
        {
            Data = dto,
            Success = true,
            Message = "Person updated successfully",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}