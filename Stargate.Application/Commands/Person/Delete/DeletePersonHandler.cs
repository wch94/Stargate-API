namespace Stargate.Application.Commands.Person.Delete;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand, DeletePersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<DeletePersonHandler> _logger;

    public DeletePersonHandler(IPersonRepository personRepository, ILogger<DeletePersonHandler> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<DeletePersonResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        if (person is null)
        {
            _logger.LogWarning($"No person found with ID {request.Id}");
            throw new NotFoundException($"No person found with ID {request.Id}");
        }

        await _personRepository.DeleteAsync(person, cancellationToken);

        return new DeletePersonResponse
        {
            Message = "Person deleted successfully",
            ResponseCode = (int)HttpStatusCode.OK, //(int)HttpStatusCode.NoContent,
            Success = true
        };
    }
}