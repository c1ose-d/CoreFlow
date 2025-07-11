namespace CoreFlow.Domain.Interfaces;

public interface IRebootListRepository
{
    Task<RebootList?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<RebootList>> GetAllAsync();
    Task AddAsync(RebootList rebootList);
    Task EditAsync(RebootList rebootList);
    Task DeleteAsync(Guid id);

    Task DeleteByRebootIdAsync(Guid id);
}