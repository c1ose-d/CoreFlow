namespace CoreFlow.Application.Interfaces;

public interface ILinuxCommandRepository
{
    Task AddAsync(LinuxCommand command, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LinuxCommand>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default);
    Task UpdateAsync(LinuxCommand command, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}