namespace Stargate.Application.Queries.GetPersonByName;

public record GetPersonByNameQuery(string Name) : IRequest<BaseResponse<PersonDto>>;