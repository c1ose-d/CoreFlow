namespace CoreFlow.Infrastructure.Reboot;

public class RebootRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IRebootRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Reboot.Reboot reboot, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Reboot entity = new()
        {
            Id = reboot.Id,
            Name = reboot.Name
        };

        _ = _context.Reboots.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Reboot.Reboot>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Reboots
            .Select(x => new Domain.Reboot.Reboot(x.Id, x.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Reboot.Reboot reboot, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Reboot? entity = await _context.Reboots.FindAsync([reboot.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = reboot.Name;

        _ = _context.Reboots.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.Reboot? entity = await _context.Reboots.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.Reboots.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Reboots
            .AnyAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase), cancellationToken);
    }
}