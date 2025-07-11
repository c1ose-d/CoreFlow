namespace CoreFlow.Domain.Interfaces;

public interface ILinuxBlockRepository
{
    Task<LinuxBlock?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<LinuxBlock>> GetAllAsync();
    Task AddAsync(LinuxBlock linuxBlock);
    Task EditAsync(LinuxBlock linuxBlock);
    Task DeleteAsync(Guid id);

    Task<bool> ExistsByNameAsync(string name);
}