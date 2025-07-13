namespace CoreFlow.Domain.Interfaces;

public interface IAppSystemRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);

    Task<AppSystem?> GetByIdAsync(Guid id);
    Task<List<AppSystem>> GetAllAsync();

    Task<List<AppSystem>> SearchAsync(string searchString);

    Task CreateAsync(AppSystem appSystem);

    Task UpdateAsync(AppSystem appSystem);

    Task DeleteAsync(Guid id);
}