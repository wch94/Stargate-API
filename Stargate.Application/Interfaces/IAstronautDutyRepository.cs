namespace Stargate.Application.Interfaces;

public interface IAstronautDutyRepository
{
    Task<List<AstronautDuty>> GetByPersonIdAsync(int personId, CancellationToken cancellationToken);
    Task AddAsync(AstronautDuty duty, CancellationToken cancellationToken);
}