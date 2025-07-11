namespace CoreFlow.Domain.Interfaces;

public interface IRebootCommandRepository
{
    Task<RebootCommand?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<RebootCommand>> GetAllAsync();
    Task AddAsync(RebootCommand rebootCommand);
    Task EditAsync(RebootCommand rebootCommand);
    Task DeleteAsync(Guid id);

    Task DeleteByRebootIdAsync(Guid id);
}