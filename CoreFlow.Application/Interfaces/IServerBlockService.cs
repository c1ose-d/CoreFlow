namespace CoreFlow.Application.Interfaces;

public interface IServerBlockService
{
    Task<ServerBlockDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ServerBlockDto>> GetByAppSystemIdAsync(Guid appSystemId);
    Task<IReadOnlyCollection<ServerBlockDto>> GetAllAsync();

    Task<IReadOnlyCollection<ServerBlockDto>> SearchAsync(string searchString);

    Task<ServerBlockDto> CreateAsync(CreateServerBlockDto dto);

    Task<ServerBlockDto> UpdateAsync(UpdateServerBlockDto dto);

    Task DeleteAsync(Guid id);
}