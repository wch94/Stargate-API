namespace Stargate.Application.Commands.Person.Update;

public record UpdatePersonEnvelope(int Id, UpdatePersonCommand Command)
    : IRequest<UpdatePersonResponse>;