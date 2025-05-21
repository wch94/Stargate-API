namespace Stargate.Application.Queries.GetAstronautDuties;

public record GetAstronautDutiesQuery(int PersonId)
    : IRequest<GetAstronautDutiesResponse>;