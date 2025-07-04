namespace CoreFlow.Application.UseCases.Reboot.AddRebootList;

public class CreateRebootListEntryHandler(IRebootListRepository repository)
{
    private readonly IRebootListRepository _repository = repository;

    public async Task<CreateRebootListEntryResult> HandleAsync(
        CreateRebootListEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        RebootListEntry entry = new(Guid.NewGuid(), command.RebootId, command.ServerId, command.ServerBlockId);

        await _repository.AddAsync(entry, cancellationToken);

        return new CreateRebootListEntryResult(entry.Id, entry.RebootId, entry.ServerId, entry.ServerBlockId);
    }
}