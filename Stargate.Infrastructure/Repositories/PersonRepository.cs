namespace Stargate.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly StargateContext _context;

    public PersonRepository(StargateContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.People
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.People
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.People
            .AsNoTracking()
            .AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        await _context.People.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
    {
        _context.People.Update(person);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
