namespace CoreFlow.Application.Interfaces;

public interface IDveBlockRepository
{
    Task AddAsync(DveBlock block, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DveBlock>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(DveBlock block, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}