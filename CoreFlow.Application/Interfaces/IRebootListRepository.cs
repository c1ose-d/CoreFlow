namespace CoreFlow.Application.Interfaces;

public interface IRebootListRepository
{
    Task AddAsync(RebootListEntry entry, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RebootListEntry>> GetByRebootIdAsync(Guid rebootId, CancellationToken cancellationToken = default);
    Task UpdateAsync(RebootListEntry entry, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}