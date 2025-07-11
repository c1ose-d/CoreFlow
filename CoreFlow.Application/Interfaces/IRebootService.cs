namespace CoreFlow.Application.Interfaces;

public interface IRebootService
{
    Task<RebootDto?> GetByIdAsync(Guid id);
    Task<IReadOnlyCollection<RebootDto>> GetAllAsync();
    Task AddAsync(RebootDto rebootDto);
    Task EditAsync(RebootDto rebootDto);
    Task DeleteAsync(Guid id);
}