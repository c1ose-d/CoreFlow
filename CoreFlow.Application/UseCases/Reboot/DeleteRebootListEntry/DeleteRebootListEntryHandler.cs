namespace CoreFlow.Application.UseCases.Reboot.DeleteRebootListEntry;

public class DeleteRebootListEntryHandler(IRebootListRepository repository)
{
    private readonly IRebootListRepository _repository = repository;

    public async Task<DeleteRebootListEntryResult> HandleAsync(
        DeleteRebootListEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteRebootListEntryResult(success);
    }
}