namespace Stargate.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly StargateContext _context;

    public PersonRepository(StargateContext context)
    {
        _context = context;
    }

    public IQueryable<Person> AsQueryable()
    {
        return _context.People
            .Include(p => p.AstronautDetail)
            .AsNoTracking();
    }

    public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.People
            .Include(p => p.AstronautDetail)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.People
            .Include(p => p.AstronautDetail)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Person?> GetByNameContainsAsync(string partialName, CancellationToken cancellationToken)
    {
        return await _context.People
            .Include(p => p.AstronautDetail)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => EF.Functions.Like(p.Name, $"%{partialName}%"), cancellationToken);
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

    public async Task DeleteAsync(Person person, CancellationToken cancellationToken)
    {
        _context.People.Remove(person);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
