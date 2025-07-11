namespace CoreFlow.Domain.Interfaces;

public interface ISystemIdRepository
{
    Task<bool> ExistsByBlockAndNameAsync(Guid blockId, string name);

    Task<List<SystemId>> GetByBlockIdAsync(Guid blockId);
    Task<SystemId?> GetByIdAsync(Guid id);

    Task AddAsync(SystemId entity);

    Task UpdateAsync(SystemId entity);

    Task DeleteAsync(Guid id);
}