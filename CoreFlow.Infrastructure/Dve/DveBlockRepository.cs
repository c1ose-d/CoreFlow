namespace CoreFlow.Infrastructure.Dve;

public class DveBlockRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IDveBlockRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Dve.DveBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveBlock entity = new()
        {
            Id = block.Id,
            Name = block.Name
        };

        _ = _context.DveBlocks.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Dve.DveBlock>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DveBlocks
            .Select(x => new Domain.Dve.DveBlock(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Dve.DveBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveBlock? entity = await _context.DveBlocks.FindAsync([block.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = block.Name;

        _ = _context.DveBlocks.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.DveBlock? entity = await _context.DveBlocks.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.DveBlocks.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.DveBlocks
            .AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}