namespace CoreFlow.Infrastructure.Repositories;

public class LinuxBlockRepository(CoreFlowContext coreFlowContext) : ILinuxBlockRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<LinuxBlock?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.LinuxBlocks.Include(x => x.LinuxCommands).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<LinuxBlock>> GetAllAsync()
    {
        return await _coreFlowContext.LinuxBlocks.AsNoTracking().Include(x => x.LinuxCommands).ToListAsync();
    }

    public async Task AddAsync(LinuxBlock linuxBlock)
    {
        _ = await _coreFlowContext.LinuxBlocks.AddAsync(linuxBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(LinuxBlock linuxBlock)
    {
        _ = _coreFlowContext.LinuxBlocks.Update(linuxBlock);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        LinuxBlock? linuxBlock = _coreFlowContext.LinuxBlocks.Find(id);

        if (linuxBlock != null)
        {
            _ = _coreFlowContext.LinuxBlocks.Remove(linuxBlock);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _coreFlowContext.LinuxBlocks.AnyAsync(b => b.Name == name);
    }
}