namespace CoreFlow.Application.DTOs.Server;

public record UpdateServerDto(Guid Id, string? IpAddress, string? HostName, string? UserName, string? Password, ServerBlockDto? ServerBlockDto);