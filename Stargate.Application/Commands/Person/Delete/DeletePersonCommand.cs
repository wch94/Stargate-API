namespace Stargate.Application.Commands.Person.Delete;

public record DeletePersonCommand(int Id) : IRequest<DeletePersonResponse>;