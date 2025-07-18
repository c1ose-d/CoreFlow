namespace CoreFlow.Application.DTOs.Server;

public record CreateServerDto(string IpAddress, string? HostName, string UserName, string Password, ServerBlockDto ServerBlockDto);