namespace CoreFlow.Infrastructure.Repositories;

public class RebootCommandRepository(CoreFlowContext coreFlowContext) : IRebootCommandRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<RebootCommand?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.RebootCommands.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<RebootCommand>> GetAllAsync()
    {
        return await _coreFlowContext.RebootCommands.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(RebootCommand rebootCommand)
    {
        _ = await _coreFlowContext.RebootCommands.AddAsync(rebootCommand);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(RebootCommand rebootCommand)
    {
        _ = _coreFlowContext.RebootCommands.Update(rebootCommand);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        RebootCommand? rebootCommand = _coreFlowContext.RebootCommands.Find(id);

        if (rebootCommand != null)
        {
            _ = _coreFlowContext.RebootCommands.Remove(rebootCommand);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }

    public async Task DeleteByRebootIdAsync(Guid id)
    {
        _coreFlowContext.RebootCommands.RemoveRange(_coreFlowContext.RebootCommands.Where(x => x.RebootId == id));
        _ = await _coreFlowContext.SaveChangesAsync();
    }
}