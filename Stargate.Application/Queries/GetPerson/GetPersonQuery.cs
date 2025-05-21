namespace Stargate.Application.Queries.GetPerson;

public record GetPersonQuery(int Id) : IRequest<GetPersonResponse>;