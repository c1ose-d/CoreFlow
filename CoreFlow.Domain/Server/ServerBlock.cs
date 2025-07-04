namespace CoreFlow.Domain.Server;

public class ServerBlock
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private readonly List<Server> _servers = [];
    public IReadOnlyCollection<Server> Servers => _servers.AsReadOnly();

    protected ServerBlock() { }

    public ServerBlock(Guid id, string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty", nameof(name));
        }

        Id = id;
        Name = name;
    }

    public void Rename(string newName)
    {
        ArgumentNullException.ThrowIfNull(newName, nameof(newName));
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Name cannot be empty", nameof(newName));
        }

        Name = newName;
    }

    public void AddServer(Server server)
    {
        ArgumentNullException.ThrowIfNull(server, nameof(server));

        if (server.BlockId != Id)
        {
            throw new InvalidOperationException("BlockId mismatch");
        }

        if (_servers.Any(s => s.IpAddress == server.IpAddress))
        {
            throw new InvalidOperationException($"Server with IP {server.IpAddress} already exists");
        }

        _servers.Add(server);
    }

    public bool RemoveServer(Guid serverId)
    {
        Server? existing = _servers.FirstOrDefault(s => s.Id == serverId);
        return existing is not null && _servers.Remove(existing);
    }
}