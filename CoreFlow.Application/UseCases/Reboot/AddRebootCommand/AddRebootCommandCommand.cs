namespace CoreFlow.Application.UseCases.Reboot.AddRebootCommand;

public record AddRebootCommandCommand(Guid RebootId, string CommandText, int ExecutionOrder);