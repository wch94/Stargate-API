namespace Stargate.Application.Commands.CreatePerson;

public record CreatePersonCommand(string Name) : IRequest<BaseResponse<PersonDto>>;