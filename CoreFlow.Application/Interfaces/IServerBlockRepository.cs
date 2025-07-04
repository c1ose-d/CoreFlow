namespace CoreFlow.Application.Interfaces;

public interface IServerBlockRepository
{
    Task AddAsync(ServerBlock block, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ServerBlock>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(ServerBlock block, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}