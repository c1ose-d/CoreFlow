namespace CoreFlow.Application.Services;

public class ServerService(IServerRepository serverRepository, IServerBlockRepository serverBlockRepository) : IServerService
{
    private readonly IServerRepository _serverRepository = serverRepository;
    private readonly IServerBlockRepository _serverBlockRepository = serverBlockRepository;

    public async Task<ServerDto?> GetByIdAsync(Guid id)
    {
        Server? server = await _serverRepository.GetByIdAsync(id);

        return server == null ? throw new Exception("Server not found.") : new ServerDto
        {
            Id = server.Id,
            IpAddress = server.IpAddress,
            HostName = server.HostName,
            UserName = server.UserName,
            Password = server.Password,
            BlockId = server.BlockId,
            ServerBlockDto = server.Block == null ? null : new ServerBlockDto
            {
                Id = server.Block.Id,
                Name = server.Block.Name
            }
        };
    }

    public async Task<IReadOnlyCollection<ServerDto>> GetAllAsync()
    {
        IReadOnlyCollection<Server> servers = await _serverRepository.GetAllAsync();

        return [.. servers.Select(server => new ServerDto
        {
            Id = server.Id,
            IpAddress = server.IpAddress,
            HostName = server.HostName,
            UserName = server.UserName,
            Password = server.Password,
            BlockId = server.BlockId,
            ServerBlockDto = server.Block == null ? null : new ServerBlockDto
            {
                Id = server.Block.Id,
                Name = server.Block.Name
            }
        })];
    }

    public async Task AddAsync(ServerDto serverDto)
    {
        ServerBlock? serverBlock = await _serverBlockRepository.GetByIdAsync(serverDto.BlockId) ?? throw new Exception("Server Block not found.");

        if (serverDto.IpAddress.Length > 200)
        {
            throw new Exception("The Ip Address must be no longer than 100 characters.");
        }

        if (serverDto.HostName != null && serverDto.HostName.Length > 200)
        {
            throw new Exception("The Host Name must be no longer than 200 characters.");
        }

        if (serverDto.UserName.Length > 50)
        {
            throw new Exception("The Host Name must be no longer than 50 characters.");
        }

        if (serverDto.Password.Length > 50)
        {
            throw new Exception("The Host Name must be no longer than 50 characters.");
        }

        Server server = new()
        {
            IpAddress = serverDto.IpAddress,
            HostName = serverDto.HostName,
            UserName = serverDto.UserName,
            Password = serverDto.Password,
            Block = serverBlock
        };

        await _serverRepository.AddAsync(server);
    }

    public async Task EditAsync(ServerDto serverDto)
    {
        Server? server = await _serverRepository.GetByIdAsync(serverDto.Id) ?? throw new Exception("Server not found.");

        if (serverDto.IpAddress.Length > 200)
        {
            throw new Exception("The Ip Address must be no longer than 100 characters.");
        }

        if (serverDto.HostName != null && serverDto.HostName.Length > 200)
        {
            throw new Exception("The Host Name must be no longer than 200 characters.");
        }

        if (serverDto.UserName.Length > 50)
        {
            throw new Exception("The Host Name must be no longer than 50 characters.");
        }

        if (serverDto.Password.Length > 50)
        {
            throw new Exception("The Host Name must be no longer than 50 characters.");
        }

        ServerBlock? serverBlock = await _serverBlockRepository.GetByIdAsync(serverDto.BlockId) ?? throw new Exception("Server Block not found.");

        server.IpAddress = serverDto.IpAddress;
        server.HostName = serverDto.HostName;
        server.UserName = serverDto.UserName;
        server.Password = serverDto.Password;
        server.Block = serverBlock;

        await _serverRepository.EditAsync(server);
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _serverRepository.GetByIdAsync(id) ?? throw new Exception("Server not found.");

        await _serverRepository.DeleteAsync(id);
    }
}