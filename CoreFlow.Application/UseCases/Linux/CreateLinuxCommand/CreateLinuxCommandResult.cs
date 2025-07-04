namespace CoreFlow.Application.UseCases.Linux.CreateLinuxCommand;

public record CreateLinuxCommandResult(Guid Id, Guid BlockId, string Name, string Content);