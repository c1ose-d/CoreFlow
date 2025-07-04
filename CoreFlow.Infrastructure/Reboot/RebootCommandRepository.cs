namespace CoreFlow.Infrastructure.Reboot;

public class RebootCommandRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : IRebootCommandRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Reboot.RebootCommand command, CancellationToken cancellationToken = default)
    {
        Persistence.Models.RebootCommand entity = new()
        {
            Id = command.Id,
            CommandText = command.CommandText,
            ExecutionOrder = command.ExecutionOrder,
            RebootId = command.RebootId
        };

        _ = _context.RebootCommands.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Reboot.RebootCommand>> GetByRebootIdAsync(Guid rebootId, CancellationToken cancellationToken = default)
    {
        return await _context.RebootCommands
            .Where(x => x.RebootId == rebootId)
            .OrderBy(x => x.ExecutionOrder)
            .Select(x => new Domain.Reboot.RebootCommand(x.Id, x.CommandText, x.ExecutionOrder, x.RebootId))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Reboot.RebootCommand command, CancellationToken cancellationToken = default)
    {
        Persistence.Models.RebootCommand? entity = await _context.RebootCommands.FindAsync([command.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.CommandText = command.CommandText;
        entity.ExecutionOrder = command.ExecutionOrder;

        _ = _context.RebootCommands.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.RebootCommand? entity = await _context.RebootCommands.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.RebootCommands.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}