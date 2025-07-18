namespace CoreFlow.Application.Services;

public class AppSystemService(IAppSystemRepository appSystemRepository, IMapper mapper) : IAppSystemService
{
    private readonly IAppSystemRepository _appSystemRepository = appSystemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<AppSystemDto?> GetByIdAsync(Guid id)
    {
        AppSystem? appSystem = await _appSystemRepository.GetByIdAsync(id);
        return _mapper.Map<AppSystemDto?>(appSystem);
    }

    public async Task<IReadOnlyCollection<AppSystemDto>> GetAllAsync()
    {
        List<AppSystem> appSystems = await _appSystemRepository.GetAllAsync();
        return [.. appSystems.Select(_mapper.Map<AppSystemDto>)];
    }

    public async Task<IReadOnlyCollection<AppSystemDto>> SearchAsync(string searchString)
    {
        List<AppSystem> appSystems = await _appSystemRepository.SearchAsync(searchString);

        return [.. appSystems.Select(_mapper.Map<AppSystemDto>)];
    }

    public async Task<AppSystemDto> CreateAsync(CreateAppSystemDto dto)
    {
        if (await _appSystemRepository.ExistsByNameAsync(dto.Name))
        {
            throw new InvalidOperationException("System name already taken.");
        }

        AppSystem appSystem = new(dto.Name, dto.ShortName);
        await _appSystemRepository.CreateAsync(appSystem);
        return _mapper.Map<AppSystemDto>(appSystem);
    }

    public async Task<AppSystemDto> UpdateAsync(UpdateAppSystemDto dto)
    {
        AppSystem appSystem = await _appSystemRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("System not found.");

        appSystem.Update(dto.Name, dto.ShortName);
        await _appSystemRepository.UpdateAsync(appSystem);
        return _mapper.Map<AppSystemDto>(appSystem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _appSystemRepository.DeleteAsync(id);
    }
}