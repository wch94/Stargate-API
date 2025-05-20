namespace Stargate.Application.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<Person>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    Task UpdateAsync(Person person, CancellationToken cancellationToken);
}