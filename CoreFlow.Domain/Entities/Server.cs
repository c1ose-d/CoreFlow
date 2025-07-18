namespace CoreFlow.Domain.Entities;

public partial class Server
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; } = null!;
    public string? HostName { get; set; }
    public string UserName { get; set; } = null!;
    public byte[] Password { get; set; } = null!;
    public Guid ServerBlockId { get; set; }

    public ServerBlock ServerBlock { get; set; } = null!;

    private Server() { }

    public Server(string ipAddress, string? hostName, string userName, byte[] password, ServerBlock serverBlock)
    {
        ValidateIpAddress(ipAddress);
        if (hostName != null)
        {
            ValidateHostName(hostName);
        }
        ValidateUserName(userName);

        Id = Guid.NewGuid();
        IpAddress = ipAddress;
        HostName = hostName;
        UserName = userName;
        Password = password;
        ServerBlock = serverBlock;
    }

    public void Update(string? ipAddress = null, string? hostName = null, string? userName = null)
    {
        if (ipAddress != null)
        {
            ValidateIpAddress(ipAddress);
            IpAddress = ipAddress;
        }

        if (hostName != null)
        {
            ValidateHostName(hostName);
            HostName = hostName;
        }

        if (userName != null)
        {
            ValidateUserName(userName);
            UserName = userName;
        }
    }

    public void ChangePassword(byte[] password)
    {
        Password = password;
    }

    public void ChangeServerBlock(ServerBlock serverBlock)
    {
        ServerBlock = serverBlock;
    }

    private static void ValidateIpAddress(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress) || ipAddress.Length > 100 || !IPAddress.TryParse(ipAddress, out _))
        {
            throw new ArgumentException("Ip address required, ≤100 chars.", nameof(ipAddress));
        }
    }

    private static void ValidateHostName(string hostName)
    {
        if (string.IsNullOrWhiteSpace(hostName) || hostName.Length > 200)
        {
            throw new ArgumentException("Host name ≤200 chars.", nameof(hostName));
        }
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName) || userName.Length > 200)
        {
            throw new ArgumentException("User name required, ≤200 chars.", nameof(userName));
        }
    }
}