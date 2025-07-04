namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootListEntry;

public class UpdateRebootListEntryHandler(IRebootListRepository repository)
{
    private readonly IRebootListRepository _repository = repository;

    public async Task<UpdateRebootListEntryResult> HandleAsync(
        UpdateRebootListEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        if (command.NewServerId is null && command.NewServerBlockId is null)
        {
            throw new InvalidOperationException("Target must specify server or server block");
        }

        RebootListEntry updated = new(
            command.Id,
            Guid.Empty,
            command.NewServerId,
            command.NewServerBlockId);

        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateRebootListEntryResult(updated.Id, updated.ServerId, updated.ServerBlockId);
    }
}