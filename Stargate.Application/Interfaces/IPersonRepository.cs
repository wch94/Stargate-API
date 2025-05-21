namespace Stargate.Application.Interfaces;

public interface IPersonRepository
{
    IQueryable<Person> AsQueryable();
    Task<List<Person>> GetAllAsync(CancellationToken cancellationToken);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Person?> GetByNameContainsAsync(string partialName, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    Task UpdateAsync(Person person, CancellationToken cancellationToken);
    Task DeleteAsync(Person person, CancellationToken cancellationToken);
}