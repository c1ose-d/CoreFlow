namespace CoreFlow.Application.Interfaces;

public interface IServerService
{
    Task<ServerDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<ServerDto>> GetAllAsync();

    Task<IReadOnlyCollection<ServerDto>> SearchAsync(string searchString);

    Task<ServerDto> CreateAsync(CreateServerDto dto);

    Task<ServerDto> UpdateAsync(UpdateServerDto dto);

    Task DeleteAsync(Guid id);
}