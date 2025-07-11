namespace CoreFlow.Application.Interfaces;

public interface ILinuxCommandService
{
    Task<LinuxCommandDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<LinuxCommandDto>> GetAllAsync();
    Task AddAsync(LinuxCommandDto linuxCommandDto);
    Task EditAsync(LinuxCommandDto linuxCommandDto);
    Task DeleteAsync(Guid id);
}