namespace CoreFlow.Infrastructure.Repositories;

public class ServerRepository(CoreFlowContext coreFlowContext) : IServerRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _coreFlowContext
            .Servers
            .AnyAsync(predicate => predicate.Id == id);
    }

    public async Task<bool> ExistsByIpAddressServerBlockIdAsync(string ipAddress, Guid serverBlockId)
    {
        return await _coreFlowContext
            .Servers
            .AnyAsync(predicate => predicate.IpAddress == ipAddress && predicate.ServerBlockId == serverBlockId);
    }

    public async Task<Server?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext
            .Servers
            .Include(navigationPropertyPath => navigationPropertyPath.ServerBlock)
            .FirstOrDefaultAsync(predicate => predicate.Id == id);
    }

    public async Task<List<Server>> GetAllAsync()
    {
        return await _coreFlowContext
            .Servers
            .Include(navigationPropertyPath => navigationPropertyPath.ServerBlock)
            .ToListAsync();
    }

    public async Task<List<Server>> SearchAsync(string searchString)
    {
        string? lower = !string.IsNullOrWhiteSpace(searchString) ? searchString.ToLower() : string.Empty;

        return await _coreFlowContext
            .Servers
            .Where(predicate => EF.Functions.Like(predicate.IpAddress, $"%{lower}") || EF.Functions.Like(predicate.HostName, $"%{lower}"))
            .Include(navigationPropertyPath => navigationPropertyPath.ServerBlock)
            .ToListAsync();
    }

    public async Task CreateAsync(Server server)
    {
        _ = await _coreFlowContext.Servers.AddAsync(server);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Server server)
    {
        _ = _coreFlowContext.Servers.Update(server);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        Server? server = await GetByIdAsync(id);
        if (server != null)
        {
            _ = _coreFlowContext.Servers.Remove(server);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}