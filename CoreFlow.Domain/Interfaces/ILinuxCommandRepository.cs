namespace CoreFlow.Domain.Interfaces;

public interface ILinuxCommandRepository
{
    Task<LinuxCommand?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<LinuxCommand>> GetAllAsync();
    Task AddAsync(LinuxCommand linuxCommand);
    Task EditAsync(LinuxCommand linuxCommand);
    Task DeleteAsync(Guid id);
}