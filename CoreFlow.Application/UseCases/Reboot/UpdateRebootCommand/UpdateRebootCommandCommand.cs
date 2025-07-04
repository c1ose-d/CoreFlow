namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootCommand;

public record UpdateRebootCommandCommand(Guid Id, string NewCommandText, int NewExecutionOrder);