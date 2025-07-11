namespace CoreFlow.Infrastructure.Repositories;

public class RebootRepository(CoreFlowContext coreFlowContext) : IRebootRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<Reboot?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.Reboots.Include(x => x.RebootCommands).Include(x => x.RebootLists).ThenInclude(x => x.Server).Include(x => x.RebootLists).ThenInclude(x => x.ServerBlock).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<Reboot>> GetAllAsync()
    {
        return await _coreFlowContext.Reboots.Include(x => x.RebootCommands).Include(x => x.RebootLists).ThenInclude(x => x.Server).Include(x => x.RebootLists).ThenInclude(x => x.ServerBlock).ToListAsync();
    }

    public async Task AddAsync(Reboot reboot)
    {
        _ = await _coreFlowContext.Reboots.AddAsync(reboot);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(Reboot reboot)
    {
        _ = _coreFlowContext.Reboots.Update(reboot);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Reboot? reboot = _coreFlowContext.Reboots.Find(id);

        if (reboot != null)
        {
            _ = _coreFlowContext.Reboots.Remove(reboot);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}