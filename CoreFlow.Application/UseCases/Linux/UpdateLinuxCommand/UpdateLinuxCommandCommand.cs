namespace CoreFlow.Application.UseCases.Linux.UpdateLinuxCommand;

public record UpdateLinuxCommandCommand(Guid Id, string NewName, string NewContent);