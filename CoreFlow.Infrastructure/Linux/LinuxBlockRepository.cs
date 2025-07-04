namespace CoreFlow.Infrastructure.Linux;

public class LinuxBlockRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : ILinuxBlockRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Linux.LinuxBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxBlock entity = new()
        {
            Id = block.Id,
            Name = block.Name
        };

        _ = _context.LinuxBlocks.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Linux.LinuxBlock>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.LinuxBlocks
            .Select(x => new Domain.Linux.LinuxBlock(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Linux.LinuxBlock block, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxBlock? entity = await _context.LinuxBlocks.FindAsync(new object[] { block.Id }, cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = block.Name;

        _ = _context.LinuxBlocks.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxBlock? entity = await _context.LinuxBlocks.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.LinuxBlocks.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.LinuxBlocks
            .AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}