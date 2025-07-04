namespace CoreFlow.Application.UseCases.Linux.CreateLinuxCommand;

public record CreateLinuxCommandCommand(Guid BlockId, string Name, string Content);