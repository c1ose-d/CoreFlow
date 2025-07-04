namespace CoreFlow.Application.Interfaces;

public interface IRebootRepository
{
    Task AddAsync(Reboot reboot, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reboot>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Reboot reboot, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}