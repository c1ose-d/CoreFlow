namespace CoreFlow.Application.UseCases.Reboot.UpdateReboot;

public class UpdateRebootHandler(IRebootRepository repository)
{
    private readonly IRebootRepository _repository = repository;

    public async Task<UpdateRebootResult> HandleAsync(
        UpdateRebootCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        if (string.IsNullOrWhiteSpace(command.NewName))
        {
            throw new ArgumentException("NewName cannot be empty", nameof(command));
        }

        Domain.Reboot.Reboot updated = new(command.Id, command.NewName);
        await _repository.UpdateAsync(updated, cancellationToken);

        return new UpdateRebootResult(updated.Id, updated.Name);
    }
}