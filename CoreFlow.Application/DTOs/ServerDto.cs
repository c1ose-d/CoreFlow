namespace CoreFlow.Application.DTOs;

public partial class ServerDto
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; } = null!;
    public string? HostName { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Guid BlockId { get; set; }

    public ServerBlockDto? ServerBlockDto { get; set; }

    public string DisplayName => HostName ?? IpAddress;
}