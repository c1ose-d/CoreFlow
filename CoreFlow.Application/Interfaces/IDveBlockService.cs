namespace CoreFlow.Application.Interfaces;

public interface IDveBlockService
{
    Task<DveBlockDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<DveBlockDto>> GetAllAsync();
    Task AddAsync(DveBlockDto dveBlockDto);
    Task EditAsync(DveBlockDto dveBlockDto);
    Task DeleteAsync(Guid id);
}