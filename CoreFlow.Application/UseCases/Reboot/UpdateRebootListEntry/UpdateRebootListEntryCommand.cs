namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootListEntry;

public record UpdateRebootListEntryCommand(Guid Id, Guid? NewServerId, Guid? NewServerBlockId);