namespace CoreFlow.Application.Services;

public class ServerBlockService(IServerBlockRepository serverBlockRepository, IServerRepository serverRepository, IAppSystemRepository appSystemRepository, IEncryptionService encryptionService, IMapper mapper) : IServerBlockService
{
    private readonly IServerBlockRepository _serverBlockRepository = serverBlockRepository;
    private readonly IServerRepository _serverRepository = serverRepository;
    private readonly IAppSystemRepository _appSystemRepository = appSystemRepository;
    private readonly IEncryptionService _encryptionService = encryptionService;
    private readonly IMapper _mapper = mapper;

    private async Task<ServerBlockDto> ToDtoAsync(Guid id)
    {
        ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        List<Guid> serverIds = [.. serverBlock.Servers.Select(selector => selector.Id)];

        List<ServerDto> serverDtos = new(serverIds.Count);
        foreach (Guid serverId in serverIds)
        {
            Server? server = await _serverRepository.GetByIdAsync(serverId);
            if (server == null)
            {
                continue;
            }
            serverDtos.Add(_mapper.Map<ServerDto>(server, opts => opts.Items["_encryptionService"] = _encryptionService));

            serverDtos = [.. serverDtos.OrderBy(keySelector => keySelector.DisplayName)];
        }
        return new ServerBlockDto(serverBlock.Id, serverBlock.Name, serverBlock.AppSystemId, serverDtos);
    }

    public async Task<ServerBlockDto?> GetByIdAsync(Guid id)
    {
        return await ToDtoAsync(id);
    }

    public async Task<IReadOnlyCollection<ServerBlockDto>> GetByAppSystemIdAsync(Guid appSystemId)
    {
        List<ServerBlock> serverBlocks = await _serverBlockRepository.GetByAppSystemIdAsync(appSystemId);
        List<ServerBlockDto> serverBlockDtos = new(serverBlocks.Count);

        foreach (ServerBlock serverBlock in serverBlocks)
        {
            serverBlockDtos.Add(await ToDtoAsync(serverBlock.Id));
        }

        serverBlockDtos = [.. serverBlockDtos.OrderBy(keySelector => keySelector.Name)];

        return serverBlockDtos;
    }

    public async Task<IReadOnlyCollection<ServerBlockDto>> GetAllAsync()
    {
        List<ServerBlock> serverBlocks = await _serverBlockRepository.GetAllAsync();
        List<ServerBlockDto> serverBlockDtos = new(serverBlocks.Count);

        foreach (ServerBlock serverBlock in serverBlocks)
        {
            serverBlockDtos.Add(await ToDtoAsync(serverBlock.Id));
        }

        serverBlockDtos = [.. serverBlockDtos.OrderBy(keySelector => keySelector.Name)];

        return serverBlockDtos;
    }

    public async Task<IReadOnlyCollection<ServerBlockDto>> SearchAsync(string searchString)
    {
        List<ServerBlock> serverBlocks = await _serverBlockRepository.SearchAsync(searchString);
        List<ServerBlockDto> serverBlockDtos = new(serverBlocks.Count);

        foreach (ServerBlock serverBlock in serverBlocks)
        {
            serverBlockDtos.Add(await ToDtoAsync(serverBlock.Id));
        }

        serverBlockDtos = [.. serverBlockDtos.OrderBy(keySelector => keySelector.Name)];

        return serverBlockDtos;
    }

    public async Task<ServerBlockDto> CreateAsync(CreateServerBlockDto dto)
    {
        if (await _serverBlockRepository.ExistsByNameAppSystemIdAsync(dto.Name, dto.AppSystemId))
        {
            throw new InvalidOperationException("Current name already in app system.");
        }

        ServerBlock serverBlock = new(dto.Name, dto.AppSystemId);

        await _serverBlockRepository.CreateAsync(serverBlock);

        return await ToDtoAsync(serverBlock.Id);
    }

    public async Task<ServerBlockDto> UpdateAsync(UpdateServerBlockDto dto)
    {
        ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(dto.Id) ?? throw new Exception("Server block not found.");

        if (await _serverBlockRepository.ExistsByNameAppSystemIdAsync(dto.Name ?? serverBlock.Name, dto.AppSystemId ?? serverBlock.AppSystemId))
        {
            throw new InvalidOperationException("Current name already in app system.");
        }

        serverBlock.Update(dto.Name);

        if (dto.AppSystemId != null)
        {
            if (await _appSystemRepository.ExistsAsync((Guid)dto.AppSystemId))
            {
                serverBlock.ChangeAppSystem(dto.AppSystemId);
            }
            else
            {
                throw new Exception("App system not found.");
            }
        }

        await _serverBlockRepository.UpdateAsync(serverBlock);
        return await ToDtoAsync(serverBlock.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _serverBlockRepository.DeleteAsync(id);
    }
}