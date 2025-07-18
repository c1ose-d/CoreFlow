namespace CoreFlow.Application.DTOs.ServerBlock;

public record ServerBlockDto(Guid Id, string Name, Guid AppSystemId, IReadOnlyCollection<ServerDto> Servers);