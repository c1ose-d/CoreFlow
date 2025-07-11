namespace CoreFlow.Application.Interfaces;

public interface IServerBlockService
{
    Task<ServerBlockDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ServerBlockDto>> GetAllAsync();
    Task AddAsync(ServerBlockDto serverBlockDto);
    Task EditAsync(ServerBlockDto serverBlockDto);
    Task DeleteAsync(Guid id);
}