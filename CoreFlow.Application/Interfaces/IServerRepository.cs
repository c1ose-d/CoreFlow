namespace CoreFlow.Application.Interfaces;

public interface IServerRepository
{
    Task AddAsync(Server server, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Server>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Server server, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}