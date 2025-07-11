namespace CoreFlow.Application.Services;

public class ServerBlockService(IServerBlockRepository serverBlockRepository) : IServerBlockService
{
    private readonly IServerBlockRepository _serverBlockRepository = serverBlockRepository;

    public async Task<ServerBlockDto?> GetByIdAsync(Guid id)
    {
        ServerBlock? serverBlock = await _serverBlockRepository.GetByIdAsync(id);

        return serverBlock == null ? throw new Exception("Server Block not found.") : new ServerBlockDto
        {
            Id = serverBlock.Id,
            Name = serverBlock.Name,
            Servers = serverBlock.Servers.Select(server => new ServerDto
            {
                Id = server.Id,
                IpAddress = server.IpAddress,
                HostName = server.HostName,
                UserName = server.UserName,
                Password = server.Password
            }).ToList().AsReadOnly()
        };
    }

    public async Task<IReadOnlyCollection<ServerBlockDto>> GetAllAsync()
    {
        IReadOnlyCollection<ServerBlock> serverBlocks = await _serverBlockRepository.GetAllAsync();

        return [.. serverBlocks.Select(serverBlock => new ServerBlockDto
        {
            Id = serverBlock.Id,
            Name = serverBlock.Name,
            Servers = serverBlock.Servers.Select(server => new ServerDto
            {
                Id = server.Id,
                IpAddress = server.IpAddress,
                HostName = server.HostName,
                UserName = server.UserName,
                Password = server.Password
            }).ToList().AsReadOnly()
        })];
    }

    public async Task AddAsync(ServerBlockDto serverBlockDto)
    {
        if (serverBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        ServerBlock serverBlock = new()
        {
            Name = serverBlockDto.Name
        };

        await _serverBlockRepository.AddAsync(serverBlock);
    }

    public async Task EditAsync(ServerBlockDto serverBlockDto)
    {
        ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(serverBlockDto.Id) ?? throw new Exception("Server Block not found.");

        if (serverBlockDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        serverBlock.Name = serverBlockDto.Name;

        await _serverBlockRepository.EditAsync(serverBlock);
    }

    public async Task DeleteAsync(Guid id)
    {
        ServerBlock serverBlock = await _serverBlockRepository.GetByIdAsync(id) ?? throw new Exception("Server Block not found.");

        if (serverBlock.Servers.Count > 0)
        {
            throw new Exception("Block has child entries.");
        }

        await _serverBlockRepository.DeleteAsync(id);
    }
}