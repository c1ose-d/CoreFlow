namespace CoreFlow.Application.Interfaces;

public interface IDveIdService
{
    Task<DveIdDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<DveIdDto>> GetAllAsync();
    Task AddAsync(DveIdDto dveIdDto);
    Task EditAsync(DveIdDto dveIdDto);
    Task DeleteAsync(Guid id);
}