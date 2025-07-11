namespace CoreFlow.Infrastructure.Repositories;

public class SystemRepository(CoreFlowContext coreFlowContext) : ISystemRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _coreFlowContext.Systems.AnyAsync(s => s.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _coreFlowContext.Systems.AnyAsync(s => s.Name == name);
    }

    public async Task<Domain.Entities.System?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.Systems.FindAsync(id).AsTask();
    }

    public async Task<List<Domain.Entities.System>> GetAllAsync()
    {
        return await _coreFlowContext.Systems.ToListAsync();
    }

    public async Task CreateAsync(Domain.Entities.System system)
    {
        _ = await _coreFlowContext.Systems.AddAsync(system);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.System system)
    {
        _ = _coreFlowContext.Systems.Update(system);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Domain.Entities.System? system = await GetByIdAsync(id);
        if (system != null)
        {
            _ = _coreFlowContext.Systems.Remove(system);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}