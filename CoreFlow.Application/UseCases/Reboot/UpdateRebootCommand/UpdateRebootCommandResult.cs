namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootCommand;

public record UpdateRebootCommandResult(Guid Id, string CommandText, int ExecutionOrder);