namespace CoreFlow.Application.UseCases.Reboot.DeleteReboot;

public class DeleteRebootHandler(IRebootRepository repository)
{
    private readonly IRebootRepository _repository = repository;

    public async Task<DeleteRebootResult> HandleAsync(
        DeleteRebootCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        bool success = await _repository.RemoveAsync(command.Id, cancellationToken);
        return new DeleteRebootResult(success);
    }
}