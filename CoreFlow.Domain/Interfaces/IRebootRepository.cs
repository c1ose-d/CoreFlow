namespace CoreFlow.Domain.Interfaces;

public interface IRebootRepository
{
    Task<Reboot?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<Reboot>> GetAllAsync();
    Task AddAsync(Reboot reboot);
    Task EditAsync(Reboot reboot);
    Task DeleteAsync(Guid id);
}