namespace CoreFlow.Application.UseCases.Server.UpdateServer;

public record UpdateServerResult(Guid Id, string IpAddress, string? HostName);