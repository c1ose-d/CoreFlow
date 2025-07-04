namespace CoreFlow.Infrastructure;

public class UnitOfWork(CoreFlowDbContext context) : IUnitOfWork
{
    private readonly CoreFlowDbContext _context = context;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}