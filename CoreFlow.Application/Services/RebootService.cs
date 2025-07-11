namespace CoreFlow.Application.Services;

public class RebootService(IRebootRepository rebootRepository, IRebootCommandRepository rebootCommandRepository, IRebootListRepository rebootListRepository, IServerRepository serverRepository, IServerBlockRepository serverBlockRepository) : IRebootService
{
    private readonly IRebootRepository _rebootRepository = rebootRepository;
    private readonly IRebootCommandRepository _rebootCommandRepository = rebootCommandRepository;
    private readonly IRebootListRepository _rebootListRepository = rebootListRepository;
    private readonly IServerRepository _serverRepository = serverRepository;
    private readonly IServerBlockRepository _serverBlockRepository = serverBlockRepository;

    public async Task<RebootDto?> GetByIdAsync(Guid id)
    {
        Reboot? reboot = await _rebootRepository.GetByIdAsync(id);

        return reboot == null ? throw new Exception("Reboot not found.") : new RebootDto
        {
            Id = reboot.Id,
            Name = reboot.Name,
            RebootCommands = [.. reboot.RebootCommands.Select(rebootCommand => new RebootCommandDto
            {
                CommandText = rebootCommand.CommandText,
                ExecutionOrder = rebootCommand.ExecutionOrder
            })],
            RebootLists = [.. reboot.RebootLists.Select(rebootList => new RebootListDto
            {
                ServerDto = rebootList.Server == null ? null : new ServerDto
                {
                    IpAddress = rebootList.Server.IpAddress,
                    HostName = rebootList.Server.HostName,
                    UserName = rebootList.Server.UserName,
                    Password = rebootList.Server.Password,
                    BlockId = rebootList.Server.BlockId,
                },
                ServerBlockDto = rebootList.ServerBlock == null ? null : new ServerBlockDto
                {
                    Name = rebootList.ServerBlock.Name,
                    Servers = rebootList.ServerBlock.Servers.Select(server => new ServerDto
                    {
                        Id = server.Id,
                        IpAddress = server.IpAddress,
                        HostName = server.HostName,
                        UserName = server.UserName,
                        Password = server.Password
                    }).ToList().AsReadOnly()
                },
            })]
        };
    }

    public async Task<IReadOnlyCollection<RebootDto>> GetAllAsync()
    {
        IReadOnlyCollection<Reboot> reboots = await _rebootRepository.GetAllAsync();

        return [.. reboots.Select(reboot => new RebootDto
        {
            Id = reboot.Id,
            Name = reboot.Name,
            RebootCommands = [.. reboot.RebootCommands.Select(rebootCommand => new RebootCommandDto
            {
                CommandText = rebootCommand.CommandText,
                ExecutionOrder = rebootCommand.ExecutionOrder
            })],
            RebootLists = [.. reboot.RebootLists.Select(rebootList => new RebootListDto
            {
                ServerDto = rebootList.Server == null ? null : new ServerDto
                {
                    IpAddress = rebootList.Server.IpAddress,
                    HostName = rebootList.Server.HostName,
                    UserName = rebootList.Server.UserName,
                    Password = rebootList.Server.Password,
                    BlockId = rebootList.Server.BlockId,
                },
                ServerBlockDto = rebootList.ServerBlock == null ? null : new ServerBlockDto
                {
                    Name = rebootList.ServerBlock.Name,
                    Servers = rebootList.ServerBlock.Servers.Select(server => new ServerDto
                    {
                        Id = server.Id,
                        IpAddress = server.IpAddress,
                        HostName = server.HostName,
                        UserName = server.UserName,
                        Password = server.Password
                    }).ToList().AsReadOnly()
                },
            })]
        })];
    }

    public async Task AddAsync(RebootDto rebootDto)
    {
        if (rebootDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        if (rebootDto.RebootCommands.Count < 1)
        {
            throw new Exception("Commands not found.");
        }

        if (rebootDto.RebootLists.Count < 1)
        {
            throw new Exception("List not found.");
        }

        for (int i = 0; i < rebootDto.RebootCommands.Count; i++)
        {
            RebootCommandDto rebootCommandDto = rebootDto.RebootCommands[i];
            if (rebootCommandDto.CommandText.Length > 200)
            {
                throw new Exception("The Name must be no longer than 200 characters.");
            }

            rebootCommandDto.ExecutionOrder = i + 1;
        }

        for (int i = 0; i < rebootDto.RebootLists.Count; i++)
        {
            RebootListDto rebootListDto = rebootDto.RebootLists[i];
            if ((rebootListDto.ServerDto != null && rebootListDto.ServerBlockDto != null) || (rebootListDto.ServerDto == null && rebootListDto.ServerBlockDto == null))
            {
                throw new Exception("Server or Server Block not found.");
            }
        }

        Reboot reboot = new()
        {
            Name = rebootDto.Name,
            RebootCommands = [.. rebootDto.RebootCommands.Select(rebootCommandDto => new RebootCommand
            {
                CommandText = rebootCommandDto.CommandText,
                ExecutionOrder = rebootCommandDto.ExecutionOrder
            })],
            RebootLists = [.. rebootDto.RebootLists.Select(rebootListDto => new RebootList
            {
                Server = rebootListDto.ServerDto == null ? null : _serverRepository.GetByIdAsync(rebootListDto.ServerDto.Id).Result,
                ServerBlock = rebootListDto.ServerBlockDto == null ? null : _serverBlockRepository.GetByIdAsync(rebootListDto.ServerBlockDto.Id).Result
            })]
        };

        await _rebootRepository.AddAsync(reboot);
    }

    public async Task EditAsync(RebootDto rebootDto)
    {
        if (rebootDto.Name.Length > 200)
        {
            throw new Exception("The Name must be no longer than 200 characters.");
        }

        if (rebootDto.RebootCommands.Count < 1)
        {
            throw new Exception("Commands not found.");
        }

        if (rebootDto.RebootLists.Count < 1)
        {
            throw new Exception("List not found.");
        }

        for (int i = 0; i < rebootDto.RebootCommands.Count; i++)
        {
            RebootCommandDto rebootCommandDto = rebootDto.RebootCommands[i];
            if (rebootCommandDto.CommandText.Length > 200)
            {
                throw new Exception("The Name must be no longer than 200 characters.");
            }

            rebootCommandDto.ExecutionOrder = i + 1;
        }

        for (int i = 0; i < rebootDto.RebootLists.Count; i++)
        {
            RebootListDto rebootListDto = rebootDto.RebootLists[i];
            if ((rebootListDto.ServerDto != null && rebootListDto.ServerBlockDto != null) || (rebootListDto.ServerDto == null && rebootListDto.ServerBlockDto == null))
            {
                throw new Exception("Server or Server Block not found.");
            }
        }

        await _rebootCommandRepository.DeleteByRebootIdAsync(rebootDto.Id);
        await _rebootListRepository.DeleteByRebootIdAsync(rebootDto.Id);

        Reboot reboot = await _rebootRepository.GetByIdAsync(rebootDto.Id) ?? throw new Exception("Reboot not found.");

        reboot.Name = rebootDto.Name;
        reboot.RebootCommands = [.. rebootDto.RebootCommands.Select(rebootCommandDto => new RebootCommand
            {
                CommandText = rebootCommandDto.CommandText,
                ExecutionOrder = rebootCommandDto.ExecutionOrder
            })];
        reboot.RebootLists = [.. rebootDto.RebootLists.Select(rebootListDto => new RebootList
            {
                Server = rebootListDto.ServerDto == null ? null : _serverRepository.GetByIdAsync(rebootListDto.ServerDto.Id).Result,
                ServerBlock = rebootListDto.ServerBlockDto == null ? null : _serverBlockRepository.GetByIdAsync(rebootListDto.ServerBlockDto.Id).Result
            })];

        await _rebootRepository.EditAsync(reboot);
    }

    public async Task DeleteAsync(Guid id)
    {
        _ = await _rebootRepository.GetByIdAsync(id) ?? throw new Exception("Reboot not found.");

        await _rebootCommandRepository.DeleteByRebootIdAsync(id);
        await _rebootListRepository.DeleteByRebootIdAsync(id);

        await _rebootRepository.DeleteAsync(id);
    }
}