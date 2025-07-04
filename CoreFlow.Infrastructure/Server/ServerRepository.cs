namespace CoreFlow.Infrastructure.Server;

public class ServerRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IServerRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Server.Server server, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Server entity = new()
        {
            Id = server.Id,
            IpAddress = server.IpAddress,
            HostName = server.HostName,
            BlockId = server.BlockId
        };

        _ = _context.Servers.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Server.Server>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default)
    {
        return await _context.Servers
            .Where(x => x.BlockId == blockId)
            .Select(x => new Domain.Server.Server(x.Id, x.IpAddress, x.HostName, x.BlockId))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Server.Server server, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Server? entity = await _context.Servers.FindAsync([server.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.IpAddress = server.IpAddress;
        entity.HostName = server.HostName;

        _ = _context.Servers.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Server? entity = await _context.Servers.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.Servers.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}