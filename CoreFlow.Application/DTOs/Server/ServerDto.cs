namespace CoreFlow.Application.DTOs.Server;

public record ServerDto(Guid Id, string IpAddress, string? HostName, string UserName, string Password, ServerBlockDto ServerBlock)
{
    public string DisplayName => HostName ?? IpAddress;
}