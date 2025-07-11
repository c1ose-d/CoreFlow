namespace CoreFlow.Domain.Interfaces;

public interface IIdBlockRepository
{
    Task<bool> ExistsByNameAndSystemIdAsync(string name, Guid systemId);

    Task<IdBlock?> GetByIdAsync(Guid id);
    Task<List<IdBlock>> GetAllAsync();

    Task AddAsync(IdBlock entity);

    Task UpdateAsync(IdBlock entity);

    Task DeleteAsync(Guid id);
}