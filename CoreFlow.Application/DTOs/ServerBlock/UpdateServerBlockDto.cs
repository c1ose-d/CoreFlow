namespace CoreFlow.Application.DTOs.ServerBlock;

public record UpdateServerBlockDto(Guid Id, string? Name, Guid? AppSystemId);