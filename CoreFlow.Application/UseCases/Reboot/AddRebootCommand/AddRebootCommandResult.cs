namespace CoreFlow.Application.UseCases.Reboot.AddRebootCommand;

public record AddRebootCommandResult(Guid Id, Guid RebootId, string CommandText, int ExecutionOrder);