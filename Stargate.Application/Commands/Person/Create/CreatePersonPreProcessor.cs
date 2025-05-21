namespace Stargate.Application.Commands.Person.Create;

public class CreatePersonPreProcessor : IRequestPreProcessor<CreatePersonCommand>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonPreProcessor(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task Process(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var exists = await _personRepository.ExistsByNameAsync(request.Name, cancellationToken);

        if (exists)
            throw new ConflictException("A person with that name already exists.");
    }
}