namespace CoreFlow.Application.UseCases.Reboot.UpdateRebootCommand;

public class UpdateRebootCommandHandler(IRebootCommandRepository repository)
{
    private readonly IRebootCommandRepository _repository = repository;

    public async Task<UpdateRebootCommandResult> HandleAsync(
        UpdateRebootCommandCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewCommandText))
        {
            throw new ArgumentException("NewCommandText cannot be empty", nameof(command));
        }

        RebootCommand updated = new(command.Id, command.NewCommandText, command.NewExecutionOrder, Guid.Empty);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateRebootCommandResult(updated.Id, updated.CommandText, updated.ExecutionOrder);
    }
}