namespace Stargate.Application.Commands.UpdatePersonByName;

public record UpdatePersonByNameCommand(string Name, string NewName)
    : IRequest<BaseResponse<PersonDto>>;