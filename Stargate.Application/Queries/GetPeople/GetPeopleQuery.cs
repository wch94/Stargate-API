namespace Stargate.Application.Queries.GetPeople;

public record GetPeopleQuery(
    string? NameFilter,
    string? SortBy,
    bool Desc,
    int Page = 1,
    int PageSize = 10
) : IRequest<GetPeopleResponse>;