namespace CoreFlow.Domain.Interfaces;

public interface IServerRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByIpAddressServerBlockIdAsync(string ipAddress, Guid serverBlockId);

    Task<Server?> GetByIdAsync(Guid id);
    Task<List<Server>> GetAllAsync();

    Task<List<Server>> SearchAsync(string searchString);

    Task CreateAsync(Server server);
    Task UpdateAsync(Server server);
    Task DeleteAsync(Guid id);
}