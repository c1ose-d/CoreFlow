namespace CoreFlow.Infrastructure.Repositories;

public class AppSystemRepository(CoreFlowContext coreFlowContext) : IAppSystemRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _coreFlowContext
            .AppSystems
            .AsNoTracking()
            .AnyAsync(predicate => predicate.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _coreFlowContext
            .AppSystems
            .AsNoTracking()
            .AnyAsync(predicate => predicate.Name == name);
    }

    public async Task<AppSystem?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext
            .AppSystems
            .FirstOrDefaultAsync(predicate => predicate.Id == id);
    }

    public async Task<List<AppSystem>> GetAllAsync()
    {
        return await _coreFlowContext
            .AppSystems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<AppSystem>> SearchAsync(string searchString)
    {
        string? lower = !string.IsNullOrWhiteSpace(searchString) ? searchString.ToLower() : string.Empty;

        return await _coreFlowContext
            .AppSystems
            .AsNoTracking()
            .Where(predicate => EF.Functions.Like(predicate.Name.ToLower(), $"%{lower}%") || EF.Functions.Like(predicate.ShortName.ToLower(), $"%{lower}%"))
            .ToListAsync();
    }

    public async Task CreateAsync(AppSystem appSystem)
    {
        _ = await _coreFlowContext.AppSystems.AddAsync(appSystem);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AppSystem appSystem)
    {
        EntityEntry<AppSystem>? local = _coreFlowContext
            .ChangeTracker
            .Entries<AppSystem>()
            .FirstOrDefault(predicate => predicate.Entity.Id == appSystem.Id);
        if (local != null)
        {
            _coreFlowContext.Entry(local.Entity).State = EntityState.Detached;
        }

        _ = _coreFlowContext.AppSystems.Update(appSystem);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        EntityEntry<AppSystem>? local = _coreFlowContext
            .ChangeTracker
            .Entries<AppSystem>()
            .FirstOrDefault(predicate => predicate.Entity.Id == id);
        if (local != null)
        {
            _coreFlowContext.Entry(local.Entity).State = EntityState.Detached;
        }

        AppSystem? appSystem = await GetByIdAsync(id);
        if (appSystem != null)
        {
            _ = _coreFlowContext.AppSystems.Remove(appSystem);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}