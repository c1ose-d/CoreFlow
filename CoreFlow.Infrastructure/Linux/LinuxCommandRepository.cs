namespace CoreFlow.Infrastructure.Linux;

public class LinuxCommandRepository(CoreFlowDbContext context, IUnitOfWork unitOfWork) : ILinuxCommandRepository
{
    private readonly CoreFlowDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Domain.Linux.LinuxCommand command, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxCommand entity = new()
        {
            Id = command.Id,
            Name = command.Name,
            Content = command.Content,
            BlockId = command.BlockId
        };

        _ = _context.LinuxCommands.Add(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Linux.LinuxCommand>> GetByBlockIdAsync(Guid blockId, CancellationToken cancellationToken = default)
    {
        return await _context.LinuxCommands
            .Where(x => x.BlockId == blockId)
            .Select(x => new Domain.Linux.LinuxCommand(x.Id, x.Name, x.Content, x.BlockId))
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Domain.Linux.LinuxCommand command, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxCommand? entity = await _context.LinuxCommands.FindAsync([command.Id], cancellationToken);
        if (entity == null)
        {
            return;
        }

        entity.Name = command.Name;
        entity.Content = command.Content;

        _ = _context.LinuxCommands.Update(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Persistence.Models.LinuxCommand? entity = await _context.LinuxCommands.FindAsync([id], cancellationToken);
        if (entity == null)
        {
            return false;
        }

        _ = _context.LinuxCommands.Remove(entity);
        _ = await _unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }
}