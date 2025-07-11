namespace CoreFlow.Infrastructure.Repositories;

public class ServerRepository(CoreFlowContext coreFlowContext) : IServerRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<Server?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.Servers.Include(x => x.Block).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<Server>> GetAllAsync()
    {
        return await _coreFlowContext.Servers.AsNoTracking().Include(x => x.Block).ToListAsync();
    }

    public async Task AddAsync(Server server)
    {
        _ = await _coreFlowContext.Servers.AddAsync(server);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(Server server)
    {
        _ = _coreFlowContext.Servers.Update(server);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Server? server = _coreFlowContext.Servers.Find(id);

        if (server != null)
        {
            _ = _coreFlowContext.Servers.Remove(server);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}