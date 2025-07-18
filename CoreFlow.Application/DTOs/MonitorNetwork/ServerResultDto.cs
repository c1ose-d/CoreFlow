namespace CoreFlow.Application.DTOs.MonitorNetwork;

public record ServerResultDto(string IpAddress, string? HostName, bool IsAlive)
{
    public string DisplayName => HostName ?? IpAddress;
}