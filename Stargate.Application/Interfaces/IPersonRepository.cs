namespace Stargate.Application.Interfaces;

public interface IPersonRepository
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
}