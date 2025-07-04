namespace CoreFlow.Application.UseCases.Reboot.AddRebootCommand;

public class AddRebootCommandHandler(IRebootCommandRepository repository)
{
    private readonly IRebootCommandRepository _repository = repository;

    public async Task<AddRebootCommandResult> HandleAsync(
        AddRebootCommandCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.CommandText))
        {
            throw new ArgumentException("CommandText cannot be empty", nameof(command));
        }

        RebootCommand rebootCommand = new(Guid.NewGuid(), command.CommandText, command.ExecutionOrder, command.RebootId);

        await _repository.AddAsync(rebootCommand, cancellationToken);

        return new AddRebootCommandResult(rebootCommand.Id, rebootCommand.RebootId, rebootCommand.CommandText, rebootCommand.ExecutionOrder);
    }
}