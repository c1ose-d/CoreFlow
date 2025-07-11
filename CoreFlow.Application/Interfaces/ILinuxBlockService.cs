namespace CoreFlow.Application.Interfaces;

public interface ILinuxBlockService
{
    Task<LinuxBlockDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<LinuxBlockDto>> GetAllAsync();
    Task AddAsync(LinuxBlockDto linuxBlockDto);
    Task EditAsync(LinuxBlockDto linuxBlockDto);
    Task DeleteAsync(Guid id);
}