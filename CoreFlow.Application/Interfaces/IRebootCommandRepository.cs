namespace CoreFlow.Application.Interfaces;

public interface IRebootCommandRepository
{
    Task AddAsync(RebootCommand command, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RebootCommand>> GetByRebootIdAsync(Guid rebootId, CancellationToken cancellationToken = default);
    Task UpdateAsync(RebootCommand command, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}