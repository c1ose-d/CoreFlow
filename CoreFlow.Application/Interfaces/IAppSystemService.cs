using CoreFlow.Application.DTOs.AppSystem;

namespace CoreFlow.Application.Interfaces;

public interface IAppSystemService
{
    Task<AppSystemDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<AppSystemDto>> GetAllAsync();

    Task<IReadOnlyCollection<AppSystemDto>> SearchAsync(string searchString);

    Task<AppSystemDto> CreateAsync(CreateAppSystemDto dto);

    Task<AppSystemDto> UpdateAsync(UpdateAppSystemDto dto);

    Task DeleteAsync(Guid id);
}