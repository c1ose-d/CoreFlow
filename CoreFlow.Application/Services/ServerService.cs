namespace CoreFlow.Application.Services;

public class ServerService(IServerRepository serverRepository, IServerBlockRepository serverBlockRepository, IMapper mapper, IEncryptionService encryptionService) : IServerService
{
    private readonly IServerRepository _serverRepository = serverRepository;
    private readonly IServerBlockRepository _serverBlockRepository = serverBlockRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IEncryptionService _encryptionService = encryptionService;

    private async Task<ServerDto> ToDtoAsync(Guid id)
    {
        Server server = await _serverRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        Guid serverBlockId = server.ServerBlockId;

        ServerBlockDto serverBlockDto = _mapper.Map<ServerBlockDto>(await _serverBlockRepository.GetByIdAsync(serverBlockId), opts => opts.Items["IncludeServers"] = false);
        return new ServerDto(server.Id, server.IpAddress, server.HostName, server.UserName, _encryptionService.Decrypt(server.Password), serverBlockDto);
    }

    public async Task<ServerDto?> GetByIdAsync(Guid id)
    {
        return await ToDtoAsync(id);
    }

    public async Task<IReadOnlyCollection<ServerDto>> GetAllAsync()
    {
        List<Server> servers = await _serverRepository.GetAllAsync();
        List<ServerDto> serverDtos = new(servers.Count);

        foreach (Server server in servers)
        {
            serverDtos.Add(await ToDtoAsync(server.Id));
        }

        return serverDtos;
    }

    public async Task<IReadOnlyCollection<ServerDto>> SearchAsync(string searchString)
    {
        List<Server> servers = await _serverRepository.SearchAsync(searchString);
        List<ServerDto> serverDtos = new(servers.Count);

        foreach (Server server in servers)
        {
            serverDtos.Add(await ToDtoAsync(server.Id));
        }

        return serverDtos;
    }

    public async Task<ServerDto> CreateAsync(CreateServerDto dto)
    {
        if (await _serverRepository.ExistsByIpAddressServerBlockIdAsync(dto.IpAddress, dto.ServerBlockDto.Id))
        {
            throw new InvalidOperationException("Current ip address already in server block.");
        }

        ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(dto.ServerBlockDto.Id) ?? throw new KeyNotFoundException();

        Server server = new(dto.IpAddress, dto.HostName, dto.UserName, _encryptionService.Encrypt(dto.Password), serverBlock);

        await _serverRepository.CreateAsync(server);

        return _mapper.Map<ServerDto>(server, opts => opts.Items["_encryptionService"] = _encryptionService);
    }

    public async Task<ServerDto> UpdateAsync(UpdateServerDto dto)
    {
        Server server = await _serverRepository.GetByIdAsync(dto.Id) ?? throw new Exception("Server block not found.");

        if (dto.IpAddress != null && dto.ServerBlockDto != null)
        {
            if (await _serverRepository.ExistsByIpAddressServerBlockIdAsync(dto.IpAddress ?? server.IpAddress, dto.ServerBlockDto?.Id ?? server.ServerBlock.Id))
            {
                throw new InvalidOperationException("Current ip address already in block.");
            }
        }

        server.Update(dto.IpAddress, dto.HostName, dto.UserName);

        if (dto.Password != null)
        {
            server.ChangePassword(_encryptionService.Encrypt(dto.Password));
        }

        if (dto.ServerBlockDto != null)
        {
            ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(dto.ServerBlockDto.Id) ?? throw new Exception("Server block not found.");
            server.ChangeServerBlock(serverBlock);
        }

        await _serverRepository.UpdateAsync(server);
        return _mapper.Map<ServerDto>(server, opts => opts.Items["_encryptionService"] = _encryptionService);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _serverRepository.DeleteAsync(id);
    }
}