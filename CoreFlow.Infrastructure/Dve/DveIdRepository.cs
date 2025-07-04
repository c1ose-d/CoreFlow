namespace CoreFlow.Infrastructure.Dve;

public class DveIdRepository : IDveIdRepository
{
    private readonly CoreFlowDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public DveIdRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(Domain.Dve.DveId dveId, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveId entity = new()
        {
            Id = dveId.Id,
            Name = dveId.Name,
            Content = dveId.Content,
            BlockId = dveId.BlockId
        };

        _ = _context.DveIds.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Dve.DveId>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default)
    {
        return await _context.DveIds
            .Where(x => x.BlockId == blockId)
            .Select(x => new Domain.Dve.DveId(x.Id, x.Name, x.Content, x.BlockId))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Dve.DveId dveId, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveId? entity = await _context.DveIds.FindAsync([dveId.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = dveId.Name;
        entity.Content = dveId.Content;

        _ = _context.DveIds.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveId? entity = await _context.DveIds.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.DveIds.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.DveIds
            .AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}