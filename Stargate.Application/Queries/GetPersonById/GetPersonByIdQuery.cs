namespace Stargate.Application.Queries.GetPersonById;

public record GetPersonByIdQuery(int Id) : IRequest<GetPersonByIdResponse>;