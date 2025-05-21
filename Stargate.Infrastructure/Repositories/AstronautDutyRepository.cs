namespace Stargate.Infrastructure.Repositories;

public class AstronautDutyRepository : IAstronautDutyRepository
{
    private readonly StargateContext _context;

    public AstronautDutyRepository(StargateContext context)
    {
        _context = context;
    }

    public async Task<List<AstronautDuty>> GetByPersonIdAsync(int personId, CancellationToken cancellationToken)
    {
        return await _context.AstronautDuties
            //.Include(d => d.Person)
            .AsNoTracking()
            .Where(d => d.PersonId == personId)
            .OrderByDescending(d => d.DutyStartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(AstronautDuty duty, CancellationToken cancellationToken)
    {
        await _context.AstronautDuties.AddAsync(duty, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}