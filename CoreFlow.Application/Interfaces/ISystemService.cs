namespace CoreFlow.Application.Interfaces;

public interface ISystemService
{
    Task<SystemDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<SystemDto>> GetAllAsync();

    Task<SystemDto> CreateAsync(CreateSystemDto dto);

    Task<SystemDto> UpdateAsync(UpdateSystemDto dto);

    Task DeleteAsync(Guid id);
}