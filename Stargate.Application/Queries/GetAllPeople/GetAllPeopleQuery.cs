namespace Stargate.Application.Queries.GetAllPeople;

public record GetAllPeopleQuery : IRequest<BaseResponse<List<PersonDto>>>;