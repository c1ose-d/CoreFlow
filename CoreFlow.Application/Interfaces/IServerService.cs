namespace CoreFlow.Application.Interfaces;

public interface IServerService
{
    Task<ServerDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ServerDto>> GetAllAsync();
    Task AddAsync(ServerDto serverDto);
    Task EditAsync(ServerDto serverDto);
    Task DeleteAsync(Guid id);
}