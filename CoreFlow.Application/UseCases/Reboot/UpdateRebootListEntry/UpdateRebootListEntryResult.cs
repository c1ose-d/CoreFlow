namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootListEntry;

public record UpdateRebootListEntryResult(Guid Id, Guid? ServerId, Guid? ServerBlockId);