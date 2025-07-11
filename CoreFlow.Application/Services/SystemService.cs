namespace CoreFlow.Application.Services;

public class SystemService(ISystemRepository systemRepository, IMapper mapper) : ISystemService
{
    private readonly ISystemRepository _systemRepository = systemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<SystemDto?> GetByIdAsync(Guid id)
    {
        Domain.Entities.System? system = await _systemRepository.GetByIdAsync(id);
        return _mapper.Map<SystemDto?>(system);
    }

    public async Task<IReadOnlyCollection<SystemDto>> GetAllAsync()
    {
        List<Domain.Entities.System> systems = await _systemRepository.GetAllAsync();
        return [.. systems.Select(_mapper.Map<SystemDto>)];
    }

    public async Task<SystemDto> CreateAsync(CreateSystemDto dto)
    {
        if (await _systemRepository.ExistsByNameAsync(dto.Name))
        {
            throw new InvalidOperationException("System name already taken.");
        }

        Domain.Entities.System system = new(dto.Name, dto.ShortName);
        await _systemRepository.CreateAsync(system);
        return _mapper.Map<SystemDto>(system);
    }

    public async Task<SystemDto> UpdateAsync(UpdateSystemDto dto)
    {
        Domain.Entities.System system = await _systemRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("System not found.");

        system.Update(dto.Name, dto.ShortName);
        await _systemRepository.UpdateAsync(system);
        return _mapper.Map<SystemDto>(system);
    }

    public Task DeleteAsync(Guid id)
    {
        return _systemRepository.DeleteAsync(id);
    }
}