namespace Stargate.Application.Commands.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, BaseResponse<PersonDto>>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<BaseResponse<PersonDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person
        {
            Name = request.Name
        };

        await _personRepository.AddAsync(person, cancellationToken);

        var dto = new PersonDto(person.Id, person.Name);

        return new BaseResponse<PersonDto>(dto)
        {
            Message = "Person created successfully",
            ResponseCode = (int)HttpStatusCode.Created
        };
    }
}