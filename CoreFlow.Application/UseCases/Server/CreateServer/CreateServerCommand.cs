namespace CoreFlow.Application.UseCases.Server.CreateServer;

public record CreateServerCommand(Guid BlockId, string IpAddress, string? HostName);