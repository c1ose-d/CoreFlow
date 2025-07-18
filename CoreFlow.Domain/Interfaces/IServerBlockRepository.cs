namespace CoreFlow.Domain.Interfaces;

public interface IServerBlockRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAppSystemIdAsync(string name, Guid appSystemId);

    Task<ServerBlock?> GetByIdAsync(Guid id);
    Task<List<ServerBlock>> GetByAppSystemIdAsync(Guid appSystemId);
    Task<List<ServerBlock>> GetAllAsync();

    Task<List<ServerBlock>> SearchAsync(string searchString);

    Task CreateAsync(ServerBlock serverBlock);

    Task UpdateAsync(ServerBlock serverBlock);

    Task DeleteAsync(Guid id);
}