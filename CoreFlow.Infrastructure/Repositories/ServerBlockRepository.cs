namespace CoreFlow.Infrastructure.Repositories;

public class ServerBlockRepository(CoreFlowContext coreFlowContext) : IServerBlockRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<ServerBlock?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.ServerBlocks.Include(x => x.Servers).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<ServerBlock>> GetAllAsync()
    {
        return await _coreFlowContext.ServerBlocks.AsNoTracking().Include(x => x.Servers).ToListAsync();
    }

    public async Task AddAsync(ServerBlock serverBlock)
    {
        _ = await _coreFlowContext.ServerBlocks.AddAsync(serverBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(ServerBlock serverBlock)
    {
        _ = _coreFlowContext.ServerBlocks.Update(serverBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        ServerBlock? serverBlock = _coreFlowContext.ServerBlocks.Find(id);

        if (serverBlock != null)
        {
            _ = _coreFlowContext.ServerBlocks.Remove(serverBlock);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}