namespace Stargate.Application.Commands.UpdatePersonByName;

public class UpdatePersonByNameHandler : IRequestHandler<UpdatePersonByNameCommand, BaseResponse<PersonDto>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public UpdatePersonByNameHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PersonDto>> Handle(UpdatePersonByNameCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByNameAsync(request.Name, cancellationToken);
        if (person is null)
            throw new NotFoundException($"No person found with name '{request.Name}'.");

        person.Name = request.NewName;

        await _personRepository.UpdateAsync(person, cancellationToken);

        var dto = _mapper.Map<PersonDto>(person);

        return new BaseResponse<PersonDto>(dto)
        {
            Message = "Person updated successfully",
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}