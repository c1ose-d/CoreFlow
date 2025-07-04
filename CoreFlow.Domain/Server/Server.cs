namespace CoreFlow.Domain.Server;

public class Server
{
    public Guid Id { get; private set; } = default!;
    public string IpAddress { get; private set; } = default!;
    public string? HostName { get; private set; }
    public Guid BlockId { get; private set; }

    protected Server() { }

    public Server(Guid id, string ipAddress, string? hostName, Guid blockId)
    {
        ArgumentNullException.ThrowIfNull(ipAddress, nameof(ipAddress));
        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            throw new ArgumentException("IP address cannot be empty", nameof(ipAddress));
        }

        Id = id;
        IpAddress = ipAddress;
        HostName = hostName;
        BlockId = blockId;
    }

    public void UpdateIp(string newIpAddress)
    {
        ArgumentNullException.ThrowIfNull(newIpAddress, nameof(newIpAddress));
        if (string.IsNullOrWhiteSpace(newIpAddress))
        {
            throw new ArgumentException("IP address cannot be empty", nameof(newIpAddress));
        }

        IpAddress = newIpAddress;
    }

    public void UpdateHostName(string? newHostName)
    {
        HostName = newHostName;
    }
}