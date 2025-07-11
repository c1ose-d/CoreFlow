namespace CoreFlow.Domain.Interfaces;

public interface IServerRepository
{
    Task<Server?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<Server>> GetAllAsync();
    Task AddAsync(Server server);
    Task EditAsync(Server server);
    Task DeleteAsync(Guid id);
}