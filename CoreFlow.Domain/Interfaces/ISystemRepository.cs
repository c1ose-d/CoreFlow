namespace CoreFlow.Domain.Interfaces;

public interface ISystemRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);

    Task<Entities.System?> GetByIdAsync(Guid id);
    Task<List<Entities.System>> GetAllAsync();

    Task CreateAsync(Entities.System system);

    Task UpdateAsync(Entities.System system);

    Task DeleteAsync(Guid id);
}