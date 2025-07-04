namespace CoreFlow.Application.UseCases.Reboot.DeleteRebootCommand;

public class DeleteRebootCommandHandler(IRebootCommandRepository repository)
{
    private readonly IRebootCommandRepository _repository = repository;

    public async Task<DeleteRebootCommandResult> HandleAsync(
        DeleteRebootCommandCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteRebootCommandResult(success);
    }
}