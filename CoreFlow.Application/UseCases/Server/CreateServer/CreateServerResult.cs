namespace CoreFlow.Application.UseCases.Server.CreateServer;

public record CreateServerResult(Guid Id, Guid BlockId, string IpAddress, string? HostName);