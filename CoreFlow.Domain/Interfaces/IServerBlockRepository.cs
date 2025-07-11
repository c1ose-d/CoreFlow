namespace CoreFlow.Domain.Interfaces;

public interface IServerBlockRepository
{
    Task<ServerBlock?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ServerBlock>> GetAllAsync();
    Task AddAsync(ServerBlock serverBlock);
    Task EditAsync(ServerBlock serverBlock);
    Task DeleteAsync(Guid id);
}