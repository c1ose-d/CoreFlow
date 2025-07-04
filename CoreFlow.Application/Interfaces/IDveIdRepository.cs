namespace CoreFlow.Application.Interfaces;

public interface IDveIdRepository
{
    Task AddAsync(DveId dveId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DveId>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default);
    Task UpdateAsync(DveId dveId, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}