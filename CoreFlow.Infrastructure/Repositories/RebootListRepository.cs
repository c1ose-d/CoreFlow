namespace CoreFlow.Infrastructure.Repositories;

public class RebootListRepository(CoreFlowContext coreFlowContext) : IRebootListRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<RebootList?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext.RebootLists.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyCollection<RebootList>> GetAllAsync()
    {
        return await _coreFlowContext.RebootLists.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(RebootList rebootList)
    {
        _ = await _coreFlowContext.RebootLists.AddAsync(rebootList);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task EditAsync(RebootList rebootList)
    {
        _ = _coreFlowContext.RebootLists.Update(rebootList);
        _ = await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        RebootList? rebootList = _coreFlowContext.RebootLists.Find(id);

        if (rebootList != null)
        {
            _ = _coreFlowContext.RebootLists.Remove(rebootList);
            _ = await _coreFlowContext.SaveChangesAsync();
        }
    }

    public async Task DeleteByRebootIdAsync(Guid id)
    {
        _coreFlowContext.RebootLists.RemoveRange(_coreFlowContext.RebootLists.Where(x => x.RebootId == id));
        _ = await _coreFlowContext.SaveChangesAsync();
    }
}