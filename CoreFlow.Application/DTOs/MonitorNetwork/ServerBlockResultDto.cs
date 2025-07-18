namespace CoreFlow.Application.DTOs.MonitorNetwork;

public record ServerBlockResultDto(string Name, IEnumerable<ServerResultDto> Servers, bool IsExpanded = true);