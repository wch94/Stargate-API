namespace Stargate.Application.Commands.UpdatePerson;

public record UpdatePersonEnvelope(int Id, UpdatePersonCommand Command)
    : IRequest<UpdatePersonResponse>;