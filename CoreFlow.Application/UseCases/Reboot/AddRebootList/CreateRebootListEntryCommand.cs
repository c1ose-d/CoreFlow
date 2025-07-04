namespace CoreFlow.Application.UseCases.Reboot.AddRebootList;

public record CreateRebootListEntryCommand(Guid RebootId, Guid? ServerId, Guid? ServerBlockId);