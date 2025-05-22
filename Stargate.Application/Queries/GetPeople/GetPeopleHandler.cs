namespace Stargate.Application.Queries.GetPeople;

public class GetPeopleHandler : IRequestHandler<GetPeopleQuery, GetPeopleResponse>
{
    private readonly IPersonRepository _repo;
    private readonly IMapper _mapper;

    public GetPeopleHandler(IPersonRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<GetPeopleResponse> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
    {
        var query = _repo.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.NameFilter))
        {
            query = query.Where(p =>
                EF.Functions.Like(p.Name, $"%{request.NameFilter}%"));
        }

        // Sorting
        query = request.SortBy switch
        {
            "id" => request.Desc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
            "name" => request.Desc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
            "currentRank" => request.Desc ? query.OrderByDescending(p => p.AstronautDetail!.CurrentRank)
                                   : query.OrderBy(p => p.AstronautDetail!.CurrentRank),
            "currentDutyTitle" => request.Desc ? query.OrderByDescending(p => p.AstronautDetail!.CurrentDutyTitle)
                                   : query.OrderBy(p => p.AstronautDetail!.CurrentDutyTitle),
            "careerStartDate" => request.Desc ? query.OrderByDescending(p => p.AstronautDetail!.CareerStartDate)
                                        : query.OrderBy(p => p.AstronautDetail!.CareerStartDate),
            "careerEndDate" => request.Desc ? query.OrderByDescending(p => p.AstronautDetail!.CareerEndDate)
                                        : query.OrderBy(p => p.AstronautDetail!.CareerEndDate),
            _ => query.OrderBy(p => p.Id)
        };

        var total = await query.CountAsync(cancellationToken);

        var people = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtoList = _mapper.Map<List<PersonAstronautDto>>(people);

        return new GetPeopleResponse
        {
            Data = dtoList,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = total,
            Message = "People retrieved",
            Success = true,
            ResponseCode = (int)HttpStatusCode.OK
        };
    }
}