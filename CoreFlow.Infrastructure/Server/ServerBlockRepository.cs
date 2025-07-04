namespace CoreFlow.Infrastructure.Server;

public class ServerBlockRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IServerBlockRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Server.ServerBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.ServerBlock entity = new()
        {
            Id = block.Id,
            Name = block.Name
        };

        _ = _context.ServerBlocks.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Server.ServerBlock>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ServerBlocks
            .Select(x => new Domain.Server.ServerBlock(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Server.ServerBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.ServerBlock? entity = await _context.ServerBlocks.FindAsync([block.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = block.Name;

        _ = _context.ServerBlocks.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.ServerBlock? entity = await _context.ServerBlocks.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.ServerBlocks.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.ServerBlocks
            .AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}