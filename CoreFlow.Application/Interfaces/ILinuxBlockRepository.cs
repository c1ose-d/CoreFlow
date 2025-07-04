namespace CoreFlow.Application.Interfaces;

public interface ILinuxBlockRepository
{
    Task AddAsync(LinuxBlock block, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LinuxBlock>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(LinuxBlock block, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}