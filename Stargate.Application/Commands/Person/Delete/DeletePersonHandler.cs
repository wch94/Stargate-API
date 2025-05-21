namespace Stargate.Application.Commands.Person.Delete;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, DeletePersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<DeletePersonResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person is null)
            throw new NotFoundException($"No person found with ID {request.Id}");

        await _personRepository.DeleteAsync(person, cancellationToken);

        return new DeletePersonResponse
        {
            Message = "Person deleted successfully",
            ResponseCode = (int)HttpStatusCode.OK, //(int)HttpStatusCode.NoContent,
            Success = true
        };
    }
}