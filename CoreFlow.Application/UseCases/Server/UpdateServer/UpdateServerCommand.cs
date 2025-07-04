namespace CoreFlow.Application.UseCases.Server.UpdateServer;

public record UpdateServerCommand(Guid Id, string NewIpAddress, string? NewHostName);