namespace CoreFlow.Infrastructure.Reboot;

public class RebootListEntryRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IRebootListRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(RebootListEntry entry, CancellationToken cancellationToken = default)
    {
        RebootList entity = new()
        {
            Id = entry.Id,
            RebootId = entry.RebootId,
            ServerId = entry.ServerId,
            ServerBlockId = entry.ServerBlockId
        };

        _ = _context.RebootLists.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RebootListEntry>> GetByRebootIdAsync(Guid rebootId, CancellationToken cancellationToken = default)
    {
        return await _context.RebootLists
            .Where(x => x.RebootId == rebootId)
            .Select(x => new RebootListEntry(x.Id, x.RebootId, x.ServerId, x.ServerBlockId))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(RebootListEntry entry, CancellationToken cancellationToken = default)
    {
        RebootList? entity = await _context.RebootLists.FindAsync([entry.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.ServerId = entry.ServerId;
        entity.ServerBlockId = entry.ServerBlockId;

        _ = _context.RebootLists.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        RebootList? entity = await _context.RebootLists.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.RebootLists.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}