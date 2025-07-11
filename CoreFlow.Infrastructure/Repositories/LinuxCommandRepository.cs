namespace CoreFlow.Infrastructure.Repositories;

public class LinuxCommandRepository(CoreFlowContext coreFlowContext) : ILinuxCommandRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<LinuxCommand?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.LinuxCommands.Include(x => x.Block).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<LinuxCommand>> GetAllAsync()
    {
        return await _coreFlowContext.LinuxCommands.AsNoTracking().Include(x => x.Block).ToListAsync();
    }

    public async Task AddAsync(LinuxCommand linuxCommand)
    {
        _ = await _coreFlowContext.LinuxCommands.AddAsync(linuxCommand);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(LinuxCommand linuxCommand)
    {
        _ = _coreFlowContext.LinuxCommands.Update(linuxCommand);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        LinuxCommand? linuxCommand = _coreFlowContext.LinuxCommands.Find(id);

        if (linuxCommand != null)
        {
            _ = _coreFlowContext.LinuxCommands.Remove(linuxCommand);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }
}