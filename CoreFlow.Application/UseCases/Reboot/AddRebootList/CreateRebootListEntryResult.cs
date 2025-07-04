namespace CoreFlow.Application.UseCases.Reboot.AddRebootList;

public record CreateRebootListEntryResult(Guid Id, Guid RebootId, Guid? ServerId, Guid? ServerBlockId);