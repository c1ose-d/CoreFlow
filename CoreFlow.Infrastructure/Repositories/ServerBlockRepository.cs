namespace CoreFlow.Infrastructure.Repositories;

public class ServerBlockRepository(CoreFlowContext coreFlowContext) : IServerBlockRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _coreFlowContext
            .ServerBlocks
            .AnyAsync(predicate => predicate.Id == id);
    }

    public async Task<bool> ExistsByNameAppSystemIdAsync(string name, Guid appSystemId)
    {
        return await _coreFlowContext
            .ServerBlocks
            .AnyAsync(predicate => predicate.Name == name && predicate.AppSystemId == appSystemId);
    }

    public async Task<ServerBlock?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext
            .ServerBlocks
            .Include(navigationPropertyPath => navigationPropertyPath.Servers)
            .FirstOrDefaultAsync(predicate => predicate.Id == id);
    }

    public async Task<List<ServerBlock>> GetByAppSystemIdAsync(Guid appSystemId)
    {
        return await _coreFlowContext
            .ServerBlocks
            .Include(navigationPropertyPath => navigationPropertyPath.Servers)
            .Where(predicate => predicate.AppSystemId == appSystemId)
            .ToListAsync();
    }

    public async Task<List<ServerBlock>> GetAllAsync()
    {
        return await _coreFlowContext
            .ServerBlocks
            .Include(navigationPropertyPath => navigationPropertyPath.Servers)
            .ToListAsync();
    }

    public async Task<List<ServerBlock>> SearchAsync(string searchString)
    {
        string? lower = !string.IsNullOrWhiteSpace(searchString) ? searchString.ToLower() : string.Empty;

        return await _coreFlowContext.ServerBlocks
            .Where(predicate => EF.Functions.Like(predicate.Name.ToLower(), $"%{lower}%") || predicate.Servers.Any(predicate => EF.Functions.Like(predicate.IpAddress.ToLower(), $"%{lower}%") || (predicate.HostName != null && EF.Functions.Like(predicate.HostName.ToLower(), $"%{lower}%"))))
            .Include(navigationPropertyPath => navigationPropertyPath.Servers.Where(predicate => EF.Functions.Like(predicate.IpAddress.ToLower(), $"%{lower}%") || (predicate.HostName != null && EF.Functions.Like(predicate.HostName.ToLower(), $"%{lower}%"))))
            .ToListAsync();
    }

    public async Task CreateAsync(ServerBlock serverBlock)
    {
        _ = await _coreFlowContext.ServerBlocks.AddAsync(serverBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ServerBlock serverBlock)
    {
        _ = _coreFlowContext.ServerBlocks.Update(serverBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        ServerBlock? serverBlock = await GetByIdAsync(id);
        if (serverBlock != null)
        {
            _ = _coreFlowContext.ServerBlocks.Remove(serverBlock);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}